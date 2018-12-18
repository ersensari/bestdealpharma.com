<template>
  <v-navigation-drawer absolute temporary v-model="collapse">
    <v-list dense>
      <v-list-tile v-for="(route, index) in routes" :key="index" :href="route.path">
        <v-list-tile-action>
          <v-icon>{{route.icon}}</v-icon>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-title>
            {{ route.display }}
          </v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>
</template>
<script>
  import { routes } from '../router/routes'

  export default {
    props: ['drawer'],
    data() {
      return {
        routes,
        collapse: null
      }
    },
    watch: {
      drawer(val) { this.collapse = val }
    }
  }
</script>
