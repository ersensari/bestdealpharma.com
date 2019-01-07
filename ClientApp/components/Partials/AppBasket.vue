<template>
  <v-fab-transition>
    <v-btn icon large fixed="fixed" bottom="bottom" left="left" color="accent" v-scroll="onScroll" v-show="fab" @click="toTop">
      <v-badge left color="red">
        <span slot="badge">6</span>
        <v-icon>
          shopping_cart
        </v-icon>
      </v-badge>
    </v-btn>
  </v-fab-transition>
</template>

<script>
export default {
  name: 'app-fab',

  data: () => ({
    fab: false
  }),

  methods: {
    onScroll () {
      if (typeof window === 'undefined') return;

      const top = window.pageYOffset ||
        document.documentElement.offsetTop ||
        0;

      this.fab = top > 300;
    },
    toTop () {
      this.$router.push({ hash: '' });
      this.$vuetify.goTo(0);
    }
  }
};
</script>
