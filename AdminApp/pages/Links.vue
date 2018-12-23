<template>
  <div id="page-links">
    <v-container grid-list-xl fluid>
      <v-layout row wrap>
        <v-toolbar color="white" lg12>
          <link-bread-crumb :items="getLinkHierarchy" @getChildItems="getChildItems"></link-bread-crumb>
        </v-toolbar>
        <v-flex lg4>
          <v-card>
            <v-flex>
              <v-toolbar flat color="white">
                <v-text-field flat
                              prepend-icon="search"
                              label="Search"
                              v-model="searchField">
                </v-text-field>
                <v-spacer></v-spacer>
                <v-btn color="primary" dark class="mb-2" @click="onNew">New Link</v-btn>
              </v-toolbar>
              <v-data-table :headers="headers"
                            :items="links"
                            :search="searchField"
                            :pagination.sync="pagination"
                            class="elevation-1">
                <template slot="items" slot-scope="props">
                  <td>{{ props.item.displayOrder }}</td>
                  <td>{{ props.item.name }}</td>
                  <td>{{ props.item.url }}</td>
                  <td class="justify-center px-0">
                    <v-tooltip top>
                      <v-btn icon small @click="onEdit(props.item)" slot="activator">
                        <v-icon small>
                          edit
                        </v-icon>
                      </v-btn>
                      <span>Edit</span>
                    </v-tooltip>
                    <v-tooltip top>
                      <v-btn icon small
                             slot="activator"
                             @click="getChildItems(props.item)">
                        <v-icon small>
                          low_priority
                        </v-icon>
                      </v-btn>
                      <span>Show Sub Links</span>
                    </v-tooltip>
                  </td>
                </template>
                <v-alert slot="no-results" :value="true" color="error" icon="warning">
                  Your search for "{{ searchField }}" found no results.
                </v-alert>
              </v-data-table>
            </v-flex>
          </v-card>
        </v-flex>
        <v-flex lg8>
          <v-alert :value="!selected" type="warning" icon="keyboard_backspace"><b>Please select an item</b></v-alert>
          <v-card v-if="selected">
            <v-toolbar flat>
              <v-btn color="primary" @click="onSave">Save<v-icon right dark>save</v-icon></v-btn>
              <v-spacer></v-spacer>
              <v-btn color="error" v-if="!isNew" @click="onDelete">Delete<v-icon right dark>delete_forever</v-icon></v-btn>
            </v-toolbar>
            <v-divider></v-divider>
            <v-flex>

              <v-form ref="form" v-model="formValid" lazy-validation>
                <v-autocomplete v-model="selected.parentId"
                                :items="parentLinks"
                                item-text="name"
                                item-value="id"
                                autocomplete
                                label="Parent Link"></v-autocomplete>
                <v-text-field v-model="selected.name"
                              :rules="[v => !!v || 'Name is required']"
                              label="Name"
                              required></v-text-field>
                <v-text-field v-model="selected.title" label="Title"></v-text-field>
                <v-text-field v-model="selected.url"
                              :rules="[v => !!v || 'Url is required']"
                              label="Url"
                              required></v-text-field>
                <v-text-field v-model="selected.displayOrder"
                              :rules="[v => !!v || 'Display Order is required',  v => /^[1-9]\d*(?:\d+)?(?:[kmbt])?$/.test(v) || 'Display Order must be a number']"
                              label="Display Order"
                              required></v-text-field>
                <v-autocomplete v-model="selected.pageId"
                                :items="pages"
                                item-text="name"
                                item-value="id"
                                clearable
                                autocomplete
                                placeholder="Please Select"
                                label="Page">
                </v-autocomplete>

              </v-form>
            </v-flex>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>
<script>
  import LinkBreadCrumb from "./Links/LinkBreadCrumb.vue"
  import { mapGetters, mapState, mapActions } from "vuex"

  export default {
    components: {
      LinkBreadCrumb
    },
    created() {
      this.$store.dispatch("links/getAll");
      this.$store.dispatch("pages/getAll");
    },
    data() {
      return {
        parentLinks: [],
        pagination: {
          rowsPerPage: 10
        },
        formValid: null,
        searchField: null,
        selected: null,
        isNew: false,
        headers: [
          { text: 'Order', sortable: true, value: 'displayOrder' },
          { text: 'Name', sortable: true, value: 'name' },
          { text: 'Url', sortable: true, value: 'url' }
        ]
      }
    },
    computed: {
      ...mapState({
        links: state => state.links.allFiltered,
        parentItem: state => state.links.parentItem,
        pages: state => state.pages.all,
        parentItem: state => state.links.parentItem
      }),
      ...mapGetters({
        getParentLinks: "links/getParentItems",
        getLinkHierarchy: "links/getLinkHierarchy"
      })
    },
    methods: {
      ...mapActions("links", [
        "getParentItems"
      ]),
      getChildItems(item) {
        this.$store.dispatch("links/getChildItems", item)
        this.selected = null
        this.isNew = false
      },
      onNew() {
        this.isNew = true
        this.$store.dispatch("links/onNew").then(response => {
          this.selected = response.data
          this.selected.parentId = this.parentItem != null ? this.parentItem.id : null
        })
        var _prnts = this.getParentLinks
        _prnts.push({ name: 'Root', id: null })
        this.parentLinks = _prnts
      },
      onEdit(item) {
        if (item.id != 0) {
          this.isNew = false
        }
        this.selected = item
        var _prnts = this.getParentLinks.filter(x => x.id != this.selected.id)
        _prnts.push({ name: 'Root', id: null })
        this.parentLinks = _prnts
      },
      onSave() {
        this.$store.dispatch("links/onSave", this.selected).then(response => {
          this.selected = response.data
          this.isNew = false
        }).catch(err => {
          console.error(err)
        })
      },
      onDelete() {
        if (this.selected) {
          this.$confirm('Do you really want to delete item?', { title: 'Warning' }).then(res => {
            if (res) {
              this.$store.dispatch("links/onDelete", this.selected.id).then(response => {
                this.selected = null
              }).catch(err => {
                console.error(e)
              })
            }
          })
        }
      }
    }
  }
</script>
