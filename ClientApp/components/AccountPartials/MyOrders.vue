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
                <v-spacer></v-spacer>
                #{{props.item.orderNumber}}
              </v-card-title>
              <v-divider></v-divider>
              <v-list dense>
                <v-list-tile>
                  <v-list-tile-content>Mobile Phone:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.mobilePhone }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>Address Line:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.addressLine }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>Zip Code:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.zipCode }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>City:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.city }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>State:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.state }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>Country:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.country }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>Sub Total:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.subTotal }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>Shipping:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.shipping }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>Total:</v-list-tile-content>
                  <v-list-tile-content class="align-end">{{ props.item.total }}</v-list-tile-content>
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
