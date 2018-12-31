<template>
  <v-app>
    <nav-menu :drawer="drawer"></nav-menu>
    <v-content>
      <v-container>
        <v-layout>
          <v-flex>
            <a href="/"><v-img src="images/logo.png" width="180" /></a>
          </v-flex>
        </v-layout>
      </v-container>
      <v-container>

        <v-btn fixed
               bottom
               right
               icon
               large
               color="accent">
          <v-badge left>
            <span slot="badge">6</span>
            <v-icon color="white">
              shopping_cart
            </v-icon>
          </v-badge>
        </v-btn>
        <v-layout>
          <v-flex>
            <v-toolbar>
              <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
              <v-toolbar-items class="hidden-sm-and-down">
                <v-btn flat v-for="link in links" :to="link.url">{{link.name}}</v-btn>
              </v-toolbar-items>
              <v-spacer></v-spacer>
              <v-text-field hide-details
                            prepend-icon="search"
                            label="Search Drug"></v-text-field>
              <v-btn icon large class="mr-3">
                <v-icon large color="grey lighten-1">keyboard</v-icon>
              </v-btn>
              <v-btn icon large>
                <v-badge left>
                  <span slot="badge">6</span>
                  <v-icon large
                          color="grey lighten-1">
                    shopping_cart
                  </v-icon>
                </v-badge>
              </v-btn>
            </v-toolbar>

          </v-flex>
        </v-layout>
      </v-container>
      <v-container>
        <v-layout>
          <router-view></router-view>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer>
      <span class="ml-2">&copy; 2018</span>
    </v-footer>
  </v-app>
</template>
<script>
  import NavMenu from './nav-menu'
  import { mapActions, mapGetters, mapState } from 'vuex'
  export default {
    components: {
      'nav-menu': NavMenu
    },
    created() {
      this.$store.dispatch("links/GetLinkHierarchy");

    },
    computed: {
      ...mapState({
        links: state => state.links.allHierarchical
      })
    },
    data: () => ({
      drawer: null
    }),
  }
</script>
<style type="text/css">
  a :hover {
    text-decoration: none !important;
  }
</style>
