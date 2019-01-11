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
                        <th>Drug Name</th>
                        <th>Quantity</th>
                        <th>Strength</th>
                        <th>Amount</th>
                        <th>Price</th>
                        <th></th>
                      </template>
                      <template slot="items" slot-scope="props">
                        <td class="text-xs-center">{{ props.item.product.title }}</td>
                        <td class="text-xs-center">{{ props.item.product.quantity }}</td>
                        <td class="text-xs-center">{{ props.item.product.strength }}</td>
                        <td class="text-xs-center">{{ props.item.amount }}</td>
                        <td class="text-xs-center">{{props.item.product.price | currency}}</td>
                        <td class="text-xs-center">
                          <v-icon small title="Delete">delete</v-icon>
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
                  <v-flex xs9 >
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
                <v-flex class="align-content-center">
                  <v-btn color="primary">CHECKOUT</v-btn>
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
      let cartCipherText = this.$cookies.get('shopping-cart');
      if (cartCipherText) {
        this.cart = this.$myUtil.decrypt(cartCipherText)
      }
    },
    methods: {
      calculateSubTotal: function () {
        return _.sumBy(this.cart, function (i) {
          return i.product.price * i.amount
        })
      }
    }
  }
</script>

<style scoped>

</style>
