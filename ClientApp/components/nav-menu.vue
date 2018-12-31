<template>
  <v-navigation-drawer absolute temporary v-model="collapse">
    <v-list dense>
      <v-list-tile v-for="link in links" :key="link.id" :to="link.url" ripple="ripple">
        <v-list-tile-content>
          <v-list-tile-title>
            <v-icon>keyboard_arrow_right</v-icon>  {{ link.name }}
          </v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>
</template>
<script>
  import { routes } from '../router/routes'
  import { mapActions, mapGetters, mapState } from 'vuex'
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
    },
    computed: {
      ...mapState({
        links: state => state.links.allHierarchical
      })
    }
  }
</script>
