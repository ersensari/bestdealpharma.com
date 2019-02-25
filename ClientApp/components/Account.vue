<template>
  <section>
    <v-toolbar
      color="deep-orange"
      hidden
      dark>
      <v-toolbar-title>
        <v-icon>account_box</v-icon>
        My Account
      </v-toolbar-title>
    </v-toolbar>
    <v-card>
      <v-layout row>
        <v-navigation-drawer permanent relative :mini-variant.sync="$vuetify.breakpoint.smAndDown">
          <v-card-title class="hidden-sm-and-down">
            <div class="headline">{{getAuthenticatedUserName}}</div>
          </v-card-title>
          <v-divider></v-divider>
          <v-list class="pt-0">
            <v-list-tile @click="$router.push({name:'account_information'})">
              <v-list-tile-action>
                <v-icon>perm_identity</v-icon>
              </v-list-tile-action>
              <v-list-tile-content class="hidden-sm-and-down">
                <v-list-tile-title>Information</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile @click="$router.push({name:'account_myorders'})">
              <v-list-tile-action>
                <v-icon>ballot</v-icon>
              </v-list-tile-action>
              <v-list-tile-content class="hidden-sm-and-down">
                <v-list-tile-title>My Orders</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile @click="$router.push({name:'account_orderrefill'})">
              <v-list-tile-action>
                <v-icon>refresh</v-icon>
              </v-list-tile-action>
              <v-list-tile-content class="hidden-sm-and-down">
                <v-list-tile-title>Order Refill</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile @click="$router.push({name:'account_addresses'})">
              <v-list-tile-action>
                <v-icon>location_on</v-icon>
              </v-list-tile-action>
              <v-list-tile-content class="hidden-sm-and-down">
                <v-list-tile-title>Shipping Addresses</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>

          </v-list>
        </v-navigation-drawer>
        <v-flex pt-3 pl-3 pb-5 mr-3>
          <router-view></router-view>
        </v-flex>
      </v-layout>
    </v-card>
  </section>
</template>

<script>
  import {mapGetters} from "vuex";

  export default {
    data() {
      return {
        mini: true
      }
    },

    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser'])
    },

  }
</script>

<style scoped>

</style>
