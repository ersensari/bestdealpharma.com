<template>
  <v-card>
    <v-card-title>
      <div class="headline">
        <v-icon large>ballot</v-icon>
        My Orders
      </div>
    </v-card-title>
    <v-divider></v-divider>
    <v-container>
      <v-layout row wrap>
        <v-flex xs12 mb-3>
          <v-expansion-panel popout>
            <v-expansion-panel-content v-for="item in orders" :key="item.id">
              <template slot="header">
                <v-layout row wrap>
                  <v-flex xs6 md4>
                    <h4>{{item.orderDate | formatdate}}</h4></v-flex>
                  <v-spacer></v-spacer>
                  <v-flex md4 class="hidden-sm-and-down">
                    <h4>#{{item.orderNumber}}</h4></v-flex>
                  <v-flex md2 class="hidden-sm-and-down"><b class="pr-2">Total:</b>${{item.total}}</v-flex>
                  <v-flex xs6 md2><h4>{{item.status | formatStatus}}</h4></v-flex>
                </v-layout>
              </template>
              <section>
                <v-layout row wrap>
                  <v-flex xs12 py-2 pr-2>
                    <v-card class="elevation-0">
                      <v-card-text>
                        <v-icon>location_on</v-icon>
                        {{item.addressLine}}, {{item.zipCode}}, {{item.city}}, {{item.state}}, {{item.country}}
                        <v-icon>phone</v-icon>
                        {{item.mobilePhone}}
                      </v-card-text>
                      <v-data-table
                        :items="item.orderDetails"
                        hide-actions
                        class="elevation-0"
                      >
                        <template slot="headers" slot-scope="props">
                          <th class="text-xs-left">Drug Name</th>
                          <th>Quantity</th>
                          <th>Strength</th>
                          <th>Amount</th>
                          <th class="text-xs-right">Price</th>
                        </template>
                        <template slot="items" slot-scope="props">
                          <td class="text-xs-left">{{ props.item.title }}</td>
                          <td class="text-xs-center">{{ props.item.quantity }}</td>
                          <td class="text-xs-center">{{ props.item.strength }}</td>
                          <td class="text-xs-center no-wrap">{{ props.item.amount }}</td>
                          <td class="text-xs-right">{{props.item.price*props.item.amount | currency}}</td>

                        </template>
                      </v-data-table>
                    </v-card>
                  </v-flex>
                  <v-flex xs12 py-2>
                    <v-card class="elevation-0">
                      <v-container fluid grid-list-md>
                        <v-layout row wrap>
                          <v-flex xs4 md2 text-xs-right>
                            <h3>Sub Total:</h3>
                          </v-flex>
                          <v-flex xs8 md10>
                            <h3>{{calculateSubTotal(item.orderDetails) | currency}}</h3>
                          </v-flex>

                          <v-flex xs4 md2 text-xs-right>
                            <h3>Shipping:</h3>
                          </v-flex>
                          <v-flex xs8 md10>
                            <h3>{{item.shipping | currency}}</h3>
                          </v-flex>
                          <v-flex xs4 md2 text-xs-right>
                            <h1>Total:</h1>
                          </v-flex>
                          <v-flex xs8 md10>
                            <h1 class="deep-orange--text">{{calculateSubTotal(item.orderDetails) + item.shipping |
                              currency}}</h1>
                          </v-flex>

                        </v-layout>
                      </v-container>


                    </v-card>
                  </v-flex>
                </v-layout>
              </section>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-flex>
      </v-layout>
    </v-container>
  </v-card>
</template>

<script>
  import {mapGetters, mapState} from 'vuex'

  export default {
    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser']),
      ...mapState({
        orders: state => state.orders.all
      })
    },
    created() {
      this.$store.dispatch('orders/getAll')
    },
    methods: {
      calculateSubTotal: function (cart) {
        return _.sumBy(cart, function (i) {
          return i.price * i.amount
        })
      },
    }
  }
</script>

<style scoped>
</style>
