<template>
  <v-fab-transition>
    <v-btn icon large fixed="fixed" bottom="bottom" left="left" color="accent" v-scroll="onScroll" v-show="fab"
           @click="toBasket">
      <v-badge left color="red">
        <span slot="badge">{{cartLength}}</span>
        <v-icon>
          shopping_cart
        </v-icon>
      </v-badge>
    </v-btn>
  </v-fab-transition>
</template>

<script>
  export default {
    name: 'app-basket',

    data: () => ({
      fab: false
    }),
    computed: {
      cartLength: function () {
        return window.getApp.cartLength
      }
    },
    methods: {
      onScroll() {
        if (typeof window === 'undefined') return;

        const top = window.pageYOffset ||
          document.documentElement.offsetTop ||
          0;

        this.fab = top > 300;
      },
      toBasket() {
        this.$router.push({name: 'shopping-cart'});
      }
    }
  };
</script>
