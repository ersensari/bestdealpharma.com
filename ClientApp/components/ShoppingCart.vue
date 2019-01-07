<template>
  <v-flex>
    <section>
      <v-parallax src="/images/sections/how-to-order.jpg" height="300">
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <v-icon class="v-icon--x-large" color="primary">shopping_cart</v-icon>
          <h1 class="blue--text text--darken-4 mb-2 display-1 text-xs-center">Shopping Cart</h1>
          <div class="blue--text text--darken-4 subheading mb-3 text-xs-center">{{id}}</div>
        </v-layout>
      </v-parallax>
    </section>
  </v-flex>
</template>

<script>
  export default {
    data() {
      return {
        id: null
      }
    },
    props: {
      productId: {
        default: null,
        required: false,
        type: String
      }
    },
    created() {
      if (this.productId && this.$router.currentRoute.name === 'add-to-shopping-cart') {
        this.id = this.productId;
      }
    }
  }
</script>

<style scoped>

</style>
