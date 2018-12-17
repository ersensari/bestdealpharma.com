<template>
  <v-app>
    <nav-menu :drawer="drawer"></nav-menu>
    <v-toolbar app fixed clipped-left>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>Application</v-toolbar-title>
    </v-toolbar>

    <v-content>
      <v-container fluid>
        <v-layout wrap>
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
      drawer: true
    }),
  }
</script>
<style>
</style>
