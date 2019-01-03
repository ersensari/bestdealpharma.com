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
                <v-btn flat v-for="link in links" :to="link.url" :key="link.id">{{link.name}}</v-btn>
              </v-toolbar-items>
              <v-spacer></v-spacer>
              <v-text-field hide-details
                            prepend-icon="search"
                            label="Search Drug"></v-text-field>
              <v-menu offset-y max-width="200">
                <v-btn icon large class="mr-3" slot="activator">
                  <v-icon large color="grey lighten-1">keyboard</v-icon>
                </v-btn>
                <v-card>
                  <v-btn small icon v-for="(item, index) in alphabet"
                         :key="index"
                         @click="onAlphabetSelected(item)">
                    {{item}}
                  </v-btn>
                </v-card>
              </v-menu>
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
      drawer: null,
      alphabet: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '#']
    }),
    methods: {
      onAlphabetSelected: (e) => {

      }
    }
  }
</script>
