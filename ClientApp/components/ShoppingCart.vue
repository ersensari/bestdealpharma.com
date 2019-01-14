<template>
  <v-flex>
    <section>
      <v-parallax src="/images/sections/shopping-cart.jpg" height="300">
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <v-icon class="v-icon--x-large" color="primary">shopping_cart</v-icon>
          <h1 class="blue--text text--darken-4 mb-2 display-1 text-xs-center">Shopping Cart</h1>
        </v-layout>
      </v-parallax>
    </section>
    <section>
      <v-container
        fluid
        grid-list-md
      >
        <v-layout row wrap>
          <v-flex xs8>
            <v-card class="elevation-1">
              <v-container
                fluid
                grid-list-md
              >
                <v-layout row wrap>
                  <v-flex>
                    <v-data-table
                      :items="cart"
                      hide-actions
                      class="elevation-0"
                    >
                      <template slot="headers" slot-scope="props">
                        <th class="text-xs-left">Drug Name</th>
                        <th>Quantity</th>
                        <th>Strength</th>
                        <th>Amount</th>
                        <th class="text-xs-right">Price</th>
                        <th></th>
                      </template>
                      <template slot="items" slot-scope="props">
                        <td class="text-xs-left">{{ props.item.product.title }}</td>
                        <td class="text-xs-center">{{ props.item.product.quantity }}</td>
                        <td class="text-xs-center">{{ props.item.product.strength }}</td>
                        <td class="text-xs-center no-wrap">
                          <v-btn small icon title="increase" @click="increase(props.item)">
                            <v-icon>arrow_drop_up</v-icon>
                          </v-btn>
                          {{ props.item.amount }}
                          <v-btn small icon title="decrease" @click="decrease(props.item)">
                            <v-icon>arrow_drop_down</v-icon>
                          </v-btn>
                        </td>
                        <td class="text-xs-right">{{props.item.product.price*props.item.amount | currency}}</td>
                        <td class="text-xs-center">
                          <v-btn icon @click="deleteItem(props.item)">
                            <v-icon title="Delete">delete</v-icon>
                          </v-btn>
                        </td>
                      </template>
                    </v-data-table>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card>
          </v-flex>
          <v-flex xs4>
            <v-card class="elevation-1">
              <v-container fluid
                           grid-list-md>
                <v-layout row wrap>
                  <v-flex xs3 text-xs-right>
                    <h3>Sub Total:</h3>
                  </v-flex>
                  <v-flex xs9>
                    <h3>{{calculateSubTotal() | currency}}</h3>
                  </v-flex>

                  <v-flex xs3 text-xs-right>
                    <h3>Shipping:</h3>
                  </v-flex>
                  <v-flex xs9>
                    <h3>{{shipping | currency}}</h3>
                  </v-flex>
                  <v-flex xs3 text-xs-right>
                    <h1>Total:</h1>
                  </v-flex>
                  <v-flex xs9>
                    <h1 class="deep-orange--text">{{calculateSubTotal() + shipping | currency}}</h1>
                  </v-flex>

                </v-layout>
              </v-container>

              <v-card-actions>
                <v-flex class="text-xs-right">
                  <v-btn class="deep-orange darken-3" dark><b>CHECKOUT</b></v-btn>
                </v-flex>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </section>
  </v-flex>
</template>

<script>
  export default {
    data() {
      return {
        cart: [],
        shipping: 0
      }
    },
    created() {
      this.cart = window.getApp.cart;
    },
    methods: {
      calculateSubTotal: function () {
        return _.sumBy(this.cart, function (i) {
          return i.product.price * i.amount
        })
      },
      increase: function (item) {
        item.amount++;
        this.$cookies.set('shopping-cart', this.$myUtil.encrypt(this.cart), '7d');
      },
      decrease: function (item) {
        if (item.amount > 1) {
          item.amount--;
          this.$cookies.set('shopping-cart', this.$myUtil.encrypt(this.cart), '7d');
        }
      },
      deleteItem: function (item) {
        var prd = this.cart.indexOf(item);
        this.cart.splice(prd, 1);
        this.$cookies.set('shopping-cart', this.$myUtil.encrypt(this.cart), '7d');
        window.getApp.cartLength = this.cart.length;

      }

    }
  }

</script>

<style scoped>

</style>
