<template>
  <div id="page-products">
    <v-container grid-list-xl fluid>
      <v-layout row wrap>
        <v-flex lg4>
          <v-card>
            <v-flex>
              <v-toolbar flat color="white">
                <v-text-field flat prepend-icon="search" label="Search" v-model="searchField"></v-text-field>
                <v-spacer></v-spacer>
                <v-btn color="primary" dark class="mb-2" @click="onNew">New Page</v-btn>
              </v-toolbar>
              <v-data-table
                :headers="headers"
                :items="products"
                :search="searchField"
                :pagination.sync="pagination"
                class="elevation-1"
              >
                <template slot="items" slot-scope="props">
                  <td>
                    <v-tooltip top>
                      <v-btn icon small @click="onEdit(props.item)" slot="activator">
                        <v-icon small>edit</v-icon>
                      </v-btn>
                      <span>Edit</span>
                    </v-tooltip>
                  </td>
                  <td>{{ props.item.title }}</td>
                  <td>{{ props.item.strength }}</td>
                  <td>{{ props.item.quantity }}</td>
                  <td>
                    <v-icon small v-if="props.item.isGeneric">check_box</v-icon>
                    <v-icon small v-if="!props.item.isGeneric">check_box_outline_blank</v-icon>
                  </td>
                  <td>
                    <v-icon small v-if="props.item.isAvailable">check_box</v-icon>
                    <v-icon small v-if="!props.item.isAvailable">check_box_outline_blank</v-icon>
                  </td>
                </template>
                <v-alert
                  slot="no-results"
                  :value="true"
                  color="error"
                  icon="warning"
                >Your search for "{{ searchField }}" found no results.</v-alert>
              </v-data-table>
            </v-flex>
          </v-card>
        </v-flex>
        <v-flex lg8>
          <v-alert :value="!selected" type="warning" icon="keyboard_backspace">
            <b>Please select an item</b>
          </v-alert>
          <v-card v-if="selected">
            <v-toolbar flat>
              <v-btn color="primary" @click="onSave">Save
                <v-icon right dark>save</v-icon>
              </v-btn>
              <v-spacer></v-spacer>
              <v-btn color="error" v-if="!isNew" @click="onDelete">Delete
                <v-icon right dark>delete_forever</v-icon>
              </v-btn>
            </v-toolbar>
            <v-divider></v-divider>
            <v-flex>
              <v-form ref="form" v-model="formValid" lazy-validation>
                <v-flex sm12>
                  <v-text-field
                    v-model="selected.title"
                    :rules="[v => !!v || 'Title is required']"
                    label="Title"
                    required
                  ></v-text-field>
                </v-flex>
                <v-flex sm12>
                  <v-text-field v-model="selected.manufacturer" label="Manufacturer"></v-text-field>
                </v-flex>
                <v-flex sm12>
                  <v-text-field
                    v-model="selected.strength"
                    :rules="[v => !!v || 'Strength is required']"
                    label="Strength"
                    required
                  ></v-text-field>
                </v-flex>
                <v-flex sm12>
                  <v-text-field
                    v-model="selected.quantity"
                    :rules="[v => !!v || 'Quantity is required']"
                    label="Quantity"
                    required
                  ></v-text-field>
                </v-flex>
                <v-flex sm12>
                  <v-text-field
                    label="Price"
                    v-model="selected.price"
                    prefix="$"
                    :rules="[v=>
                    !!v || 'Price is required',
                    v => /^-?(?:0|0\.\d*|[1-9]\d*\.?\d*)$/.test(v) || 'Price must be decimal'
                    ]"
                  >></v-text-field>
                </v-flex>
                <v-flex>
                  <v-checkbox label="Is Generic" v-model="selected.isGeneric"></v-checkbox>
                </v-flex>
                <v-flex>
                  <v-checkbox label="Prescription Required" v-model="selected.onlyRx"></v-checkbox>
                </v-flex>
                <v-flex>
                  <v-checkbox label="Is Available" v-model="selected.isAvailable"></v-checkbox>
                </v-flex>
                <v-flex sm12>
                  <quill-editor v-model="selected.htmlContent"></quill-editor>
                </v-flex>
              </v-form>
            </v-flex>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>
<script>
import { mapGetters, mapState, mapActions } from "vuex";
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";
import "quill/dist/quill.bubble.css";
import { quillEditor } from "vue-quill-editor";
const storeNamespace = "products/";
export default {
  components: {
    quillEditor
  },

  created() {
    this.$store.dispatch(storeNamespace + "getAll");
  },

  data() {
    return {
      pagination: {
        rowsPerPage: 10
      },
      formValid: null,
      searchField: null,
      selected: null,
      isNew: false,
      headers: [
        { text: "", sortable: false },
        { text: "Title", sortable: true, value: "title", width: "200" },
        { text: "Strength", sortable: true, value: "strength" },
        { text: "Quantity", sortable: true, value: "quantity" },
        { text: "Is Generic", sortable: true, value: "isGeneric" },
        { text: "Is Available", sortable: true, value: "isAvailable" }
      ]
    };
  },
  computed: {
    ...mapState({
      products: state => state.products.all
    })
  },
  methods: {
    onNew() {
      this.isNew = true;
      this.$store.dispatch(storeNamespace + "onNew").then(response => {
        this.selected = response.data;
      });
    },
    onEdit(item) {
      if (item.id != 0) {
        this.isNew = false;
      }
      this.selected = item;
    },
    onSave() {
      this.$store
        .dispatch(storeNamespace + "onSave", this.selected)
        .then(response => {
          this.selected = response.data;
          this.isNew = false;
        });
    },
    onDelete() {
      if (this.selected) {
        this.$confirm("Do you really want to delete item?", {
          title: "Warning"
        }).then(res => {
          if (res) {
            this.$store
              .dispatch(storeNamespace + "onDelete", this.selected.id)
              .then(response => {
                this.selected = null;
              });
          }
        });
      }
    }
  }
};
</script>
<style lang="stylus" scoped>
.quill {
  height: 320px;
}
</style>
