<template>
  <v-app>
    <nav-menu :drawer="drawer"></nav-menu>
    <section>
      <v-container>
        <v-layout row>
          <v-flex xs6 md3>
            <a href="/">
              <v-img src="/images/logo.png"/>
            </a>
          </v-flex>
          <v-spacer></v-spacer>
          <user-partial></user-partial>
        </v-layout>
      </v-container>
    </section>
    <v-content>
      <v-container fill-height>
        <v-layout column>
          <v-toolbar class="elevation-2 mb-3" color="secondary" dark>
            <v-toolbar-side-icon @click.stop="drawer = !drawer" class="hidden-lg-and-up"></v-toolbar-side-icon>
            <v-toolbar-items class="hidden-md-and-down">
              <v-btn flat v-for="link in routes.filter(x=>!x.hideOnMenu)" :to="link.path" :key="link.name">
                {{link.display
                | capitalize}}
              </v-btn>
            </v-toolbar-items>
            <v-spacer></v-spacer>
            <v-text-field hide-details
                          prepend-icon="search"
                          v-model="searchText"
                          @keyup.native.enter="onSearch"
                          label="Search for your medications"></v-text-field>

            <v-menu offset-y max-width="200">
              <v-btn icon large class="mr-3" slot="activator">
                <v-icon large color="lighten-1">keyboard</v-icon>
              </v-btn>
              <v-card>
                <v-btn small icon v-for="(item, index) in alphabet"
                       :key="index"
                       @click="onSearchByLetter(item)">
                  {{item}}
                </v-btn>
              </v-card>
            </v-menu>
            <v-menu offset-y max-width="500">
              <v-btn icon large slot="activator">
                <v-fab-transition>
                  <v-badge left color="red">
                    <span slot="badge">{{cartLength}}</span>
                    <v-icon large
                            color="lighten-1">
                      shopping_cart
                    </v-icon>
                  </v-badge>
                </v-fab-transition>
              </v-btn>
              <v-card class="elevation-1">
                <vue-perfect-scrollbar class="drawer-menu--scroll" :settings="scrollSettings">
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
                    </template>
                    <template slot="items" slot-scope="props">
                      <td class="text-xs-left">{{ props.item.product.title }}</td>
                      <td class="text-xs-center">{{ props.item.product.quantity }}</td>
                      <td class="text-xs-center">{{ props.item.product.strength }}</td>
                      <td class="text-xs-center">{{ props.item.amount }}</td>
                      <td class="text-xs-right">{{props.item.product.price*props.item.amount | currency}}</td>
                    </template>
                  </v-data-table>
                </vue-perfect-scrollbar>
                <v-card-actions>
                  <v-btn flat dark class="deep-orange darken-2" to="/shopping-cart">SHOPPING CART</v-btn>
                </v-card-actions>

              </v-card>
            </v-menu>
          </v-toolbar>
          <router-view></router-view>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer>
      <span class="ml-2">bestdealpharma.com &copy; 2019</span>
    </v-footer>
    <app-fab></app-fab>
    <app-basket></app-basket>
  </v-app>
</template>
<script>
  import NavMenu from './components/Partials/LeftMenuPartial'
  import AppFab from './components/Partials/AppFab'
  import AppBasket from './components/Partials/AppBasket'
  import UserPartial from './components/Partials/UserPartial'

  import {routes} from './router/routes'
  import AppEvents from './event'

  import VuePerfectScrollbar from 'vue-perfect-scrollbar'
import {mapState} from 'vuex'

  export default {
    components: {
      'nav-menu': NavMenu,
      'app-fab': AppFab,
      'app-basket': AppBasket,
      'vue-perfect-scrollbar': VuePerfectScrollbar,
      'user-partial': UserPartial
    },
    created () {
      this.$store.dispatch('links/GetLinkHierarchy')
      AppEvents.forEach(item => {
        this.$on(item.name, item.callback)
      })

      window.getApp = this

      this.cartLength = this.cart.length

      let cartCipherText = this.$cookies.get('shopping-cart')
      if (cartCipherText) {
        this.cart = this.$myUtil.decrypt(cartCipherText)
        this.cartLength = this.cart.length
      }
    },
    computed: {
      ...mapState({
        links: state => state.links.allHierarchical
      })
    },
    data: () => ({
      scrollSettings: {
        maxScrollbarLength: 160
      },
      cart: [],
      cartLength: 0,
      searchText: '',
      routes,
      drawer: null,
      alphabet: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']
    }),
    methods:
      {
        onSearchByLetter: function (letter) {
          window.getApp.$emit('APP_SEARCH_DRUG', letter)
        },
        onSearch: function () {
          window.getApp.$emit('APP_SEARCH_DRUG', this.searchText)
        }
      }
  }
</script>
