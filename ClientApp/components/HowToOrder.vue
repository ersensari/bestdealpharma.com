<template>
  <v-flex>
    <section>
      <v-parallax src="images/sections/how-to-order.jpg" height="300">
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <img src="images/mini-logo.png" alt="Best Deal Pharma Online Pharmacy" height="81">
          <h1 class="blue--text text--darken-4 mb-2 display-1 text-xs-center">How To Order</h1>
          <div class="blue--text text--darken-4 subheading mb-3 text-xs-center">Follow these steps to get started</div>
        </v-layout>
      </v-parallax>
    </section>
    <section>
      <v-layout column
                wrap
                class="my-5"
                align-center>
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
                    Please use the
                    <router-link to="/new-order">search form</router-link>
                    to find your recipe or try to find drug name in alphabetical drug list. And select your dosage and
                    quantity from the product listings and click on Add to Cart.
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
                    Once you added your medications to your shopping cart, place your order online. A valid prescription
                    is required prior to the dispensing your order, click on "Continue Checkout".
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
                    After you have completed your online order, send us a copy of your prescription(s). Delivery time is
                    approximately 2 to 3 weeks. We guarantee that your order will arrive within 30
                    days.
                  </v-card-text>
                </v-card>
              </v-flex>
              <v-flex xs12>
                <div class="text-xs-center">
                  <h2 class="headline">Notes of Importance</h2>
                  <span class="subheading">
                    If your order contains prescription items, a valid prescription written by a medical practitioner licensed to practice in your area of residence is required. All prescriptions must be written in English. Once an order for a prescription medication has been placed, we require the Rx be faxed, emailed, uploaded via our site, or mailed to us. A mailed prescription can take up to 2 weeks to be received. Because of this, we recommend faxing, emailing or uploading your prescription. Please do not forward your prescription before placing an order. No changes or cancellations can be made to your prescription order once it has been placed.
                  </span>
                </div>
              </v-flex>
            </v-layout>
          </v-container>
        </v-flex>
      </v-layout>
    </section>
  </v-flex>
</template>
<script>
  export default {

  }
</script>
