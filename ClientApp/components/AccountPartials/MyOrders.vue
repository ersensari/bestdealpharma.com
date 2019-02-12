<template>
  <v-flex>
    <v-card>
      <v-card-title>
        <div class="headline">
          <v-icon large>ballot</v-icon>
          My Orders
        </div>
      </v-card-title>
      <v-divider></v-divider>
      <v-container fluid grid-list-md>
        <v-data-iterator
          :items="orders"
          content-tag="v-layout"
          hide-actions
          row
          wrap
        >
          <v-flex
            slot="item"
            slot-scope="props"
            xs12
            sm6
            md4
            lg3
          >
            <v-card>
              <v-card-title>
                <h4>
                  {{props.item.orderDate | formatdate}}
                </h4>
              </v-card-title>
              <v-divider></v-divider>
              <v-list dense>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.mobilePhone }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.addressLine }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.zipCode }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.city }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.state }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.country }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.subTotal }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.shipping }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ props.item.total }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile-action>
                  <v-flex class="text-xs-center pa-3">
                  <v-btn icon>
                    <v-icon>visibility</v-icon>
                  </v-btn>
                  </v-flex>
                </v-list-tile-action>
              </v-list>
            </v-card>
          </v-flex>
        </v-data-iterator>
      </v-container>
    </v-card>
  </v-flex>

</template>

<script>
  import {mapGetters, mapState} from "vuex";

  export default {
    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser']),
      ...mapState({
          orders: state =>
            state.orders.all
        }
      )
    },
    created() {
      this.$store.dispatch('orders/getAll');
    }
  }
</script>

<style scoped>

</style>
