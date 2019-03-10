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
      <v-layout row wrap>
        <v-flex xs12 md8 py-2 pr-2>
          <v-card class="elevation-1">
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
          </v-card>
        </v-flex>
        <v-flex xs6 md4 py-2>
          <v-card class="elevation-1">
            <v-container fluid
                         grid-list-md>
              <v-layout row wrap>
                <v-flex xs4 text-xs-right>
                  <h3>Sub Total:</h3>
                </v-flex>
                <v-flex xs8>
                  <h3>{{calculateSubTotal() | currency}}</h3>
                </v-flex>

                <v-flex xs4 text-xs-right class="align-self-center">
                  <h3>Shipping:</h3>
                </v-flex>
                <v-flex xs8>
                  <v-select dense :items="shippingItems" v-model="shipping" :return-object="true"
                            hint="Please select shipping method!" persistent-hint>
                  </v-select>
                </v-flex>
                <v-flex xs4 text-xs-right>
                  <h1>Total:</h1>
                </v-flex>
                <v-flex xs8>
                  <h1 class="deep-orange--text">{{calculateGrandTotal()| currency}}</h1>
                </v-flex>

              </v-layout>
            </v-container>

            <v-card-actions>
              <v-flex class="text-xs-right">
                <v-btn class="darken-3 deep-orange white--text" @click="goCheckout"
                       :disabled="shipping==null"><b>CHECKOUT</b>
                </v-btn>
              </v-flex>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </section>
  </v-flex>
</template>

<script>
  import {mapState} from 'vuex'

  export default {
    data() {
      return {
        cart: [],
        shipping: null
      }
    },
    computed: {
      ...mapState({
        shippingItems: state => state.shippings.all
      })
    },

    created() {
      this.cart = window.getApp.cart;
      this.$store.dispatch('shippings/getAll')

    },
    methods: {
      calculateSubTotal: function () {
        return _.sumBy(this.cart, function (i) {
          return i.product.price * i.amount
        })
      },
      calculateGrandTotal: function () {
        if (this.shipping != null) {
          const subtotal = _.sumBy(this.cart, function (i) {
            return i.product.price * i.amount
          });
          return subtotal + this.shipping.price;
        } else {
          return 'Please select shipping method'
        }
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

      },
      goCheckout: function () {
        const shippingid = this.shipping.id;
        this.$router.push({name: 'checkout', params: {shippingId: shippingid}})
      }

    }
  }

</script>

<style scoped>

</style>
