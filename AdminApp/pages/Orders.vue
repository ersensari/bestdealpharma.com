<template>
  <div id="page-orders">
    <v-container grid-list-md fluid>
      <v-layout row wrap>
        <v-flex xs5>
          <v-card>
            <v-flex>
              <v-toolbar flat color="white">
                <v-select dense :items="orderStatuses" v-model="status"
                          hint="Please select order Status!" persistent-hint>
                </v-select>
              </v-toolbar>
              <v-data-table
                :headers="headers"
                :items="orders"
                :pagination.sync="pagination"
                class="elevation-0"
              >
                <template slot="items" slot-scope="props">
                  <tr :active="selected" @click="selected = props.item">
                    <td>{{ props.item.orderDate | formatdate}}</td>
                    <td>{{ props.item.orderNumber }}</td>
                    <td>{{ props.item.total | currency }}</td>
                    <td>{{ props.item.city}}</td>
                  </tr>
                </template>
                <v-alert
                  slot="no-results"
                  :value="true"
                  color="error"
                  icon="warning"
                >There is no results.
                </v-alert>
              </v-data-table>
            </v-flex>
          </v-card>
        </v-flex>
        <v-flex xs7>
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
            <v-layout row wrap v-if="selected">
              <v-flex xs12 py-2 pr-2>
                <v-card class="elevation-0">
                  <v-select dense :items="orderStatuses" v-model="selected.status" class="mr-2 ml-2"
                            hint="Change Order Status!" persistent-hint>
                  </v-select>
                  <v-textarea class="ml-2 mr-2" v-model="selected.shippingLink" label="Shipping Trace Link"
                              v-if="selected.status==2"></v-textarea>
                  <v-textarea class="ml-2 mr-2" v-model="selected.serviceExplanation" label="Explanation"></v-textarea>
                  <v-textarea class="ml-2 mr-2" v-model="selected.customerExplanation" disabled
                              label="Order Explanation"></v-textarea>
                  <v-card-text>
                    <v-icon>location_on</v-icon>
                    {{selected.addressLine}}, {{selected.zipCode}}, {{selected.city}}, {{selected.state}},
                    {{selected.country}}
                    <v-icon>phone</v-icon>
                    {{selected.mobilePhone}}
                  </v-card-text>
                  <v-data-table
                    :items="selected.orderDetails"
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
                        <h3>{{calculateSubTotal(selected.orderDetails) | currency}}</h3>
                      </v-flex>

                      <v-flex xs4 md2 text-xs-right>
                        <h3>Shipping:</h3>
                      </v-flex>
                      <v-flex xs8 md10>
                        <h3>{{selected.shipping | currency}}</h3>
                      </v-flex>
                      <v-flex xs4 md2 text-xs-right>
                        <h2>Total:</h2>
                      </v-flex>
                      <v-flex xs8 md10>
                        <h2 class="deep-orange--text">{{calculateSubTotal(selected.orderDetails) + selected.shipping |
                          currency}}</h2>
                      </v-flex>

                    </v-layout>
                  </v-container>


                </v-card>
              </v-flex>
            </v-layout>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
  import {mapGetters, mapState} from 'vuex'

  export default {
    data() {
      return {
        pagination: {
          rowsPerPage: 10
        },
        selected: null,
        status: 0,
        orderStatuses: [
          {value: 0, text: 'Waiting Payment'},
          {value: 1, text: 'Preparing'},
          {value: 2, text: 'Shipped'},
          {value: 3, text: 'Canceled'}
        ],
        headers: [
          {text: "Order Date", sortable: true, value: "orderDate"},
          {text: "Order Number", sortable: true, value: "orderNumber"},
          {text: "Total", sortable: true, value: "total"},
          {text: "City", sortable: true, value: "city"}
        ]
      }
    },
    watch: {
      status: function (value) {
        this.$store.dispatch('orders/getAllForAdmin', value)
      }
    },
    computed: {
      ...mapState({
        orders: state => state.orders.all
      })
    },
    created() {
      this.$store.dispatch('orders/getAllForAdmin', this.status)
    },
    methods: {
      calculateSubTotal: function (cart) {
        return _.sumBy(cart, function (i) {
          return i.price * i.amount
        })
      },
      onSave:function () {

      }
    }

  }
</script>

<style>

</style>
