<template>

  <v-flex>
    <section>
      <v-parallax src="images/sections/section1.jpg" height="600">
        <v-layout column
                  align-center
                  justify-center
                  class="white--text">
          <img src="images/mini-logo.png" alt="Best Deal Pharma Online Pharmacy" height="81">
          <h1 class="white--text mb-2 display-1 text-xs-center">Best Deal Pharma</h1>
          <div class="subheading mb-3 text-xs-center">Welcome to your online pharmacy</div>
          <v-btn class="blue lighten-2 mt-5"
                 dark
                 large
                 to="/new-order">
            Get Started
          </v-btn>
        </v-layout>
      </v-parallax>
    </section>

    <section>
      <v-layout column
                wrap
                class="my-5"
                align-center>
        <v-flex xs12 sm4 class="my-3">
          <div class="text-xs-center">
            <h2 class="headline">Welcome to your one-stop online pharmacy</h2>
            <span class="subheading">
              Follow these steps to get started
            </span>
          </div>
        </v-flex>
        <v-flex xs12>
          <v-container grid-list-xl>
            <v-layout row wrap align-center>
              <v-flex xs12 md4>
                <v-card class="elevation-0 transparent">
                  <v-card-text class="text-xs-center">
                    <v-icon x-large class="blue--text text--lighten-2">search</v-icon>
                  </v-card-text>
                  <v-card-title primary-title class="layout justify-center">
                    <div class="headline text-xs-center">1. Find your medication</div>
                  </v-card-title>
                  <v-card-text>
                    Please use the <router-link to="/new-order">search form</router-link> to find your recipe or try to find drug name in alphabetical drug list.
                  </v-card-text>
                </v-card>
              </v-flex>
              <v-flex xs12 md4>
                <v-card class="elevation-0 transparent">
                  <v-card-text class="text-xs-center">
                    <v-icon x-large class="blue--text text--lighten-2">shopping_basket</v-icon>
                  </v-card-text>
                  <v-card-title primary-title class="layout justify-center">
                    <div class="headline">2. Create Your Order</div>
                  </v-card-title>
                  <v-card-text>
                    Once you added your medications to your shopping cart, place your order online. A valid prescription is required prior to the dispensing your order. We can call your doctor free of charge.
                  </v-card-text>
                </v-card>
              </v-flex>
              <v-flex xs12 md4>
                <v-card class="elevation-0 transparent">
                  <v-card-text class="text-xs-center">
                    <v-icon x-large class="blue--text text--lighten-2">local_shipping</v-icon>
                  </v-card-text>
                  <v-card-title primary-title class="layout justify-center">
                    <div class="headline text-xs-center">3. Receive Your Order</div>
                  </v-card-title>
                  <v-card-text>
                    Delivery time is approximately 2 to 3 weeks. We guarantee that your order will arrive within 30 days.
                  </v-card-text>
                </v-card>
              </v-flex>
            </v-layout>
          </v-container>
        </v-flex>
      </v-layout>
    </section>

    <section>
      <v-parallax src="images/sections/section2.jpg" height="380">
        <v-layout column align-center justify-center>
          <div class="headline white--text mb-3 text-xs-center">The best way to buy your prescription</div>
          <em>Economical, Safety, Easy </em>
          <v-btn class="blue lighten-2 mt-5"
                 dark
                 large
                 to="/new-order">
            Get Started
          </v-btn>
        </v-layout>
      </v-parallax>
    </section>

    <section>
      <v-container grid-list-xl>
        <v-layout row wrap justify-center class="my-5">
          <v-flex xs12 sm4>
            <v-card class="elevation-0 transparent">
              <v-card-title primary-title class="layout justify-center">
                <div class="headline">Company info</div>
              </v-card-title>
              <v-card-text>
                Cras facilisis mi vitae nunc lobortis pharetra. Nulla volutpat tincidunt ornare.
                Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                Nullam in aliquet odio. Aliquam eu est vitae tellus bibendum tincidunt. Suspendisse potenti.
              </v-card-text>
            </v-card>
          </v-flex>
          <v-flex xs12 sm4 offset-sm1>
            <v-card class="elevation-0 transparent">
              <v-card-title primary-title class="layout justify-center">
                <div class="headline">Contact us</div>
              </v-card-title>
              <v-card-text>
                Cras facilisis mi vitae nunc lobortis pharetra. Nulla volutpat tincidunt ornare.
              </v-card-text>
              <v-list class="transparent">
                <v-list-tile>
                  <v-list-tile-action>
                    <v-icon class="blue--text text--lighten-2">phone</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>777-867-5309</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-action>
                    <v-icon class="blue--text text--lighten-2">place</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>Chicago, US</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-action>
                    <v-icon class="blue--text text--lighten-2">email</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>john@vuetifyjs.com</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </section>
  </v-flex>
</template>
<script>
  export default {
    data() {
      return {}
    }
  }
</script>
<style>
</style>
