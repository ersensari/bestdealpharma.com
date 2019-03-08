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
      <v-container fluid>
        <v-layout row wrap>
          <v-flex xs12 mb-3>
            <v-expansion-panel popout>
              <v-expansion-panel-content v-for="item in orders" :key="item.id">
                <template slot="header">
                  <v-layout row wrap>
                    <v-flex xs4>
                      <h4>{{item.orderDate | formatdate}}</h4></v-flex>
                    <v-spacer class="hidden-sm-and-down"></v-spacer>
                    <v-flex xs4 class="hidden-sm-and-down">
                      <h4>#{{item.orderNumber}}</h4></v-flex>
                    <v-flex md4 class="hidden-sm-and-down">${{item.total}}</v-flex>
                  </v-layout>
                </template>
                <v-card>
                  <v-card-text>
                    <v-list dense>
                      <v-list-tile>
                        <v-list-tile-content>Mobile Phone:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.mobilePhone }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>Address Line:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.addressLine }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>Zip Code:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.zipCode }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>City:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.city }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>State:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.state }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>Country:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.country }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>Sub Total:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.subTotal }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>Shipping:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.shipping }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile>
                        <v-list-tile-content>Total:</v-list-tile-content>
                        <v-list-tile-content class="align-end">{{ item.total }}</v-list-tile-content>
                      </v-list-tile>
                      <v-list-tile-action>
                        <v-flex class="text-xs-center pa-3">
                          <v-btn icon>
                            <v-icon>visibility</v-icon>
                          </v-btn>
                        </v-flex>
                      </v-list-tile-action>
                    </v-list>
                  </v-card-text>
                </v-card>
              </v-expansion-panel-content>
            </v-expansion-panel>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card>
  </v-flex>
</template>

<script>
import { mapGetters, mapState } from 'vuex'

export default {
  computed: {
    ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser']),
    ...mapState({
      orders: state => state.orders.all
    })
  },
  created() {
    this.$store.dispatch('orders/getAll')
  }
}
</script>

<style scoped>
</style>
