<template>
  <div id="appRoot">
    <v-app class="app" v-if="systemBusy">
      <v-content>
        <v-container fluid fill-height class="loading">
          <v-layout align-center justify-center>
            <v-flex xs12 sm8 md4>
              <v-progress-circular :size="100"
                                   fixed
                                   color="primary"
                                   indeterminate></v-progress-circular>
            </v-flex>
          </v-layout>
        </v-container>
      </v-content>
      </v-app>
      <template v-if="!$route.meta.public" v-show="!systemBusy">
        <v-app id="inspire" class="app">
          <app-drawer class="app--drawer"></app-drawer>
          <app-toolbar class="app--toolbar"></app-toolbar>
          <v-content>
            <!-- Page Header -->
            <page-header v-if="$route.meta.breadcrumb"></page-header>
            <div class="page-wrapper">
              <router-view></router-view>
            </div>
            <!-- App Footer -->
            <v-footer height="auto" fixed absolute>
              <span class="caption ml-2">bestdealpharma.com &copy; {{ new Date().getFullYear() }}</span>
              <v-spacer></v-spacer>
              <span class="caption mr-1"> Make With Love </span>
              <v-icon color="pink" small class="mr-3">favorite</v-icon>
            </v-footer>
          </v-content>
          <!-- Go to top -->
          <app-fab></app-fab>
        </v-app>
      </template>
      <template v-else>
        <transition>
          <keep-alive>
            <router-view></router-view>
          </keep-alive>
        </transition>
      </template>
  </div>
</template>
<script>
  import AppDrawer from '@/components/AppDrawer';
  import AppToolbar from '@/components/AppToolbar';
  import AppFab from '@/components/AppFab';
  import PageHeader from '@/components/PageHeader';
  import menu from '@/api/menu';
  import AppEvents from './event';

  import { mapState } from 'vuex'

  export default {
    components: {
      AppDrawer,
      AppToolbar,
      AppFab,
      PageHeader
    },
    data: () => ({
      expanded: true,
      rightDrawer: false,
      systemBusy: false
    }),
    created() {
      AppEvents.forEach(item => {
        this.$on(item.name, item.callback);
      });
      window.getApp = this;
    },
    methods: {

    },

  };
</script>


<style lang="stylus" scoped>

  .setting-fab
    top:50% right:0 border-radius:0
      .page-wrapper
        min-height:calc(100vh - 64px - 50px - 81px)

  .loading
    background-color:#000
</style>
