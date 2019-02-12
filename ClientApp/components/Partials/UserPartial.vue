<template>
  <v-flex class="text-xs-right" v-if="!isAuthenticated">
    <v-btn flat class="text-capitalize" to="/login">
      <fa-icon icon="sign-in-alt" class="mr-2" size="lg"/>
      Login
    </v-btn>
    <v-btn flat class="text-capitalize" to="/register">
      <v-icon class="mr-2">person_add</v-icon>
      Create Account
    </v-btn>
  </v-flex>
  <v-menu offset-y origin="center center" :nudge-bottom="10" transition="scale-transition" v-else-if="isAuthenticated">
    <v-btn flat slot="activator">
      <v-icon class="mr-1">account_box</v-icon>
      {{getAuthenticatedUserName}}
    </v-btn>
    <v-list class="pa-0">
      <v-list-tile v-for="(item,index) in items" :to="!item.href ? { name: item.name } : null" :href="item.href"
                   @click="item.click" ripple="ripple" :disabled="item.disabled" :target="item.target" rel="noopener"
                   :key="index">
        <v-list-tile-action v-if="item.icon">
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-title>{{ item.title }}</v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-menu>
</template>

<script>
  import {mapGetters} from "vuex";

  export default {
    data: () => ({
      items: [
        {
          icon: 'account_balance_wallet',
          name: 'account',
          title: 'My Account',
          click: (e) => {
          }
        },
        {
          icon: 'exit_to_app',
          href: '#',
          title: 'Logout',
          click: (e) => {
            window.getApp.$emit('APP_LOGOUT');
          }
        }
      ],
    }),
    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated'])
    },
    created() {
      this.$store.dispatch('user/getAuthenticatedUser')
    }
  }
</script>
