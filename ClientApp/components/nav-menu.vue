<template>
  <v-navigation-drawer absolute temporary v-model="collapse">
    <v-list dense>
      <v-list-tile v-for="link in routes" :key="link.name" :to="link.path" ripple="ripple">
        <v-list-tile-action>
          <v-icon v-if="link.icon">{{link.icon}}</v-icon>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-title>
            {{ link.display | capitalize}}
          </v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>
</template>
<script>
  import {routes} from '../router/routes'
  import {mapActions, mapGetters, mapState} from 'vuex'

  export default {
    props: ['drawer'],
    data() {
      return {
        routes,
        collapse: null
      }
    },
    watch: {
      drawer(val) {
        this.collapse = val
      }
    },
    computed: {
      ...mapState({
        links: state => state.links.allHierarchical
      })
    }
  }
</script>
