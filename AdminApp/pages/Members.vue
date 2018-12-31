<template>
  <div id="page-members">
    <v-container grid-list-xl fluid>
      <v-layout row wrap>
        <v-flex lg4>
          <v-card>
            <v-flex>
              <v-toolbar flat color="white">
                <v-text-field flat prepend-icon="search" label="Search" v-model="searchField"></v-text-field>
              </v-toolbar>
              <v-data-table
                :headers="headers"
                :items="members"
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
                  <td>{{ props.item.person.name }}</td>
                  <td>{{ props.item.person.surname }}</td>
                  <td>{{ props.item.user.email }}</td>
                  <td>{{ props.item.person.mobilePhone }}</td>
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
            </v-toolbar>
            <v-divider></v-divider>
            <v-flex grid-list-md>
              <h4>{{ selected.person.name }} {{ selected.person.surname }}</h4>
              <v-divider></v-divider>

              <v-layout row wrap>
                <v-flex xs2 font-weight-bold>Email:</v-flex>
                <v-flex xs10>{{ selected.user.email }}</v-flex>

                <v-flex xs2 font-weight-bold>Mobile Phone:</v-flex>
                <v-flex xs10>{{ selected.person.mobilePhone }}</v-flex>

                <v-flex xs2 font-weight-bold>Home Phone:</v-flex>
                <v-flex xs10>{{ selected.person.homePhone }}</v-flex>

                <v-flex xs2 font-weight-bold>Date of Birth:</v-flex>
                <v-flex xs10>{{ selected.person.birthDate | formatdate }}</v-flex>

                <v-flex xs2 font-weight-bold>Address:</v-flex>
                <v-flex xs10>{{ selected.person.address }}</v-flex>

                <v-flex xs2 font-weight-bold>Zip/Postal Code:</v-flex>
                <v-flex xs10>{{ selected.person.zipCode }}</v-flex>

                <v-flex xs2 font-weight-bold>State:</v-flex>
                <v-flex xs10>{{ selected.person.state }}</v-flex>

                <v-flex xs2 font-weight-bold>City:</v-flex>
                <v-flex xs10>{{ selected.person.city }}</v-flex>

                <v-flex xs2 font-weight-bold>Country:</v-flex>
                <v-flex xs10>{{ selected.person.country }}</v-flex>
              </v-layout>
            </v-flex>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>
<script>
import { mapGetters, mapState, mapActions } from "vuex";
const storeNamespace = "members/";
export default {
  created() {
    this.$store.dispatch(storeNamespace + "getAll");
  },

  data() {
    return {
      pagination: {
        rowsPerPage: 10
      },
      searchField: null,
      selected: null,
      headers: [
        { text: "", sortable: false },
        { text: "Name", sortable: true, value: "name" },
        { text: "Surname", sortable: true, value: "surname" },
        { text: "Email", sortable: true, value: "Email" },
        { text: "Mobile Phone", sortable: true, value: "mobilePhone" }
      ]
    };
  },
  computed: {
    ...mapState({
      members: state => state.members.all
    })
  },
  methods: {
    onEdit(item) {
      this.selected = item;
    },
    onSave() {
      this.$store
        .dispatch(storeNamespace + "onSave", this.selected)
        .then(response => {
          this.selected = response.data;
        });
    }
  }
};
</script>
