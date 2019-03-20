<template xmlns:v-slot="http://www.w3.org/1999/XSL/Transform">
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
                  <v-flex xs5 md3>
                    <h4>{{item.orderDate | formatdate}}</h4></v-flex>
                  <v-spacer></v-spacer>
                  <v-flex md4 class="hidden-sm-and-down">
                    <h4>#{{item.orderNumber}}</h4></v-flex>
                  <v-flex md2 class="hidden-sm-and-down"><b class="pr-2">Total:</b>${{item.total}}</v-flex>
                  <v-flex xs6 md2><h4>{{item.status | formatStatus}}</h4></v-flex>
                  <v-flex xs1 md1 v-if="item.status==0">
                    [<a href="javascript:void(0)" @click="cancelOrder(item.id)" title="Cancel Order">
                    Cancel
                  </a>]
                  </v-flex>
                  <v-flex xs1 md1 v-if="item.status==3 || item.status==2">
                    [<a href="javascript:void(0)" @click="archiveOrder(item.id)" title="Move To Archive">
                    Archive
                  </a>]
                  </v-flex>

                </v-layout>
              </template>
              <section>
                <v-layout row wrap>
                  <v-flex xs12 py-2 pr-2>
                    <v-card class="elevation-0">
                      <v-card-text>
                        <blockquote class="blockquote">
                          {{item.customerExplanation}}
                        </blockquote>
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
                          <v-flex xs12 md6>
                            <v-layout row wrap>
                              <v-flex xs4 text-xs-right>
                                <h3>Sub Total:</h3>
                              </v-flex>
                              <v-flex xs8>
                                <h3>{{calculateSubTotal(item.orderDetails) | currency}}</h3>
                              </v-flex>
                              <v-flex xs4 text-xs-right>
                                <h3>Shipping:</h3>
                              </v-flex>
                              <v-flex xs8>
                                <h3>{{item.shipping | currency}}</h3>
                              </v-flex>
                              <v-flex xs4 text-xs-right>
                                <h2>Total:</h2>
                              </v-flex>
                              <v-flex xs8>
                                <h2 class="deep-orange--text">{{calculateSubTotal(item.orderDetails) + item.shipping
                                  | currency}}</h2>
                              </v-flex>
                            </v-layout>
                          </v-flex>
                          <v-flex xs12 md6>
                            <v-list>
                              <v-list-tile>
                                <v-list-tile-avatar>
                                  <v-icon>account_circle</v-icon>
                                </v-list-tile-avatar>
                                <h3>{{item.person.name}} {{item.person.surname}}</h3>
                              </v-list-tile>
                            </v-list>
                            <v-list>
                              <v-list-tile>
                                <v-list-tile-avatar>
                                  <v-icon>location_on</v-icon>
                                </v-list-tile-avatar>
                                {{item.addressLine}}, {{item.zipCode}}, {{item.city}}, {{item.state}},
                                {{item.country}}
                              </v-list-tile>
                            </v-list>
                            <v-list>
                              <v-list-tile>
                                <v-list-tile-avatar>
                                  <v-icon>phonelink_ring</v-icon>
                                </v-list-tile-avatar>
                                {{item.mobilePhone}}
                              </v-list-tile>
                            </v-list>
                            <v-list>
                              <v-list-tile>
                                <v-list-tile-avatar>
                                  <v-icon>alternate_email</v-icon>
                                </v-list-tile-avatar>
                                {{item.person.user.email}}
                              </v-list-tile>
                            </v-list>
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
    components: {},

    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser']),
      ...mapState({
        orders: state => {
          if (state.orders.all) {
            return state.orders.all.filter(x => !x.archived)
          } else
            return [];
        }
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
      cancelOrder: function (id) {
        this.$confirm('Do you really want to cancel this order?', {title: 'Warning'}).then(res => {
          if (res) {
            this.$store.dispatch("orders/onCancel", id).then(response => {
              const order = this.orders.find((x) => {
                return x.id === id
              });
              order.status = 3;
              this._vm.$toastr('info', 'The order has been canceled');
            }).catch(e => {
              console.error(e)
            })
          }
        })
      },
      archiveOrder: function (id) {
        this.$confirm('Do you really want to move this order?', {title: 'Warning'}).then(res => {
          if (res) {
            this.$store.dispatch("orders/onArchived", id).then(response => {
              const order = this.orders.find((x) => {
                return x.id === id
              });
              order.archived = 1;
            }).catch(e => {
              console.error(e)
            })
          }
        })
      }
    }
  }
</script>

<style scoped>
</style>
