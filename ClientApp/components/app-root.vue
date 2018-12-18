<template>
  <v-app>
    <nav-menu :drawer="drawer"></nav-menu>
    <v-content>
      <v-container>
        <v-layout>
          <v-flex>
            <v-img src="images/logo.png" width="240" />
          </v-flex>
        </v-layout>
      </v-container>
      <v-container>
        <v-layout>
          <v-flex>
            <v-toolbar>
              <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
              <v-spacer></v-spacer>
              <v-toolbar-items class="hidden-sm-and-down">
                <v-btn flat>Link One</v-btn>
                <v-btn flat>Link Two</v-btn>
                <v-btn flat>Link Three</v-btn>
              </v-toolbar-items>
            </v-toolbar>
          </v-flex>
        </v-layout>
      </v-container>
      <v-container>
        <v-layout>
          <v-flex>
            <router-view></router-view>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
    <v-footer app fixed>
      <span>&copy; 2017</span>
    </v-footer>
  </v-app>
</template>
<script>
  import NavMenu from './nav-menu'

  export default {
    components: {
      'nav-menu': NavMenu
    },

    data: () => ({
      drawer: null
    }),
  }
</script>
<style>
</style>
