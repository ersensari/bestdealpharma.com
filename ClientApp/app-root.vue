<template>
  <v-app>
    <nav-menu :drawer="drawer"></nav-menu>
    <v-content>
      <v-container>
        <v-layout row>
          <v-flex xs1>
            <a href="/">
              <v-img src="/images/logo.png" width="180"/>
            </a>
          </v-flex>
          <v-spacer></v-spacer>
          <v-btn flat class="text-capitalize">
            <fa-icon icon="sign-in-alt" class="mr-2" size="lg"/>
            Login
          </v-btn>
          <v-btn flat class="text-capitalize">
            <v-icon class="mr-2">person_add</v-icon>
            Create Account
          </v-btn>

        </v-layout>
      </v-container>
      <v-container>

        <v-toolbar class="elevation-2">
          <v-toolbar-side-icon @click.stop="drawer = !drawer" class="hidden-lg-and-up"></v-toolbar-side-icon>
          <v-toolbar-items class="hidden-md-and-down">
            <v-btn flat v-for="link in routes.filter(x=>!x.hideOnMenu)" :to="link.path" :key="link.name">{{link.display
              | capitalize}}
            </v-btn>
          </v-toolbar-items>
          <v-spacer></v-spacer>
          <v-text-field hide-details
                        prepend-icon="search"
                        v-model="searchText"
                        @keyup.native.enter="onSearch"
                        label="Search Drug"></v-text-field>
          <v-menu offset-y max-width="200">
            <v-btn icon large class="mr-3" slot="activator">
              <v-icon large color="grey lighten-1">keyboard</v-icon>
            </v-btn>
            <v-card>
              <v-btn small icon v-for="(item, index) in alphabet"
                     :key="index"
                     @click="onSearchByLetter(item)">
                {{item}}
              </v-btn>
            </v-card>
          </v-menu>
          <v-btn icon large to="/shopping-cart">
            <v-fab-transition>
              <v-badge left color="red">
                <span slot="badge">{{cartCount}}</span>
                <v-icon large
                        color="grey lighten-1">
                  shopping_cart
                </v-icon>
              </v-badge>
            </v-fab-transition>
          </v-btn>
        </v-toolbar>
        <v-layout class="mt-3">
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
  import {routes} from './router/routes'
  import AppEvents from './event'
  import Util from '@/util'

  import {mapState} from 'vuex'

  export default {
    components: {
      'nav-menu': NavMenu,
      'app-fab': AppFab,
      'app-basket': AppBasket
    },
    created() {
      this.$store.dispatch("links/GetLinkHierarchy");
      AppEvents.forEach(item => {
        this.$on(item.name, item.callback);
      });

      window.getApp = this;
    },
    computed: {
      ...mapState({
        links: state => state.links.allHierarchical
      }),
      basket: {
        get: function () {
          return Util.getCookie('shopping-cart')
        },

        set: function (value) {
          let cart = Util.getCookie('shopping-cart');
          if (!cart) {
            cart = []
          }

          if (!cart.find(c => id === value.id)) {
            cart.push(value)
          }
          Util.setCookie('shopping-cart', cart, 60)
        },
      },
      cartCount: {
        get: function () {
          return this.basket ? this.basket.length : 0
        }
      }
    },
    data: () => ({
      searchText: '',
      routes,
      drawer: null,
      alphabet: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']
    }),
    methods: {
      onSearchByLetter: function (letter) {
        window.getApp.$emit('APP_SEARCH_DRUG', letter)
      },
      onSearch: function () {
        window.getApp.$emit('APP_SEARCH_DRUG', this.searchText)

      }
    }
  }
</script>
