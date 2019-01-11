<template>
  <v-flex>
    <section>
      <v-parallax src="/images/sections/how-to-order.jpg" height="300">
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <img src="/images/mini-logo.png" alt="Best Deal Pharma Online Pharmacy" height="81">
          <h1 class="blue--text text--darken-4 mb-2 display-1 text-xs-center">New Order</h1>
          <div class="blue--text text--darken-4 subheading mb-3 text-xs-center">Economical, Safety, Easy</div>
        </v-layout>
      </v-parallax>
    </section>

    <section>
      <v-card class="elevation-0">
        <v-container
          fluid
          grid-list-md
        >
          <v-layout row wrap>
            <v-flex class="xs8 sm10 md4">
              <v-text-field hide-details
                            prepend-icon="search"
                            v-model="searchText"
                            @keyup.native.enter="onSearch"
                            label="Search Drug"></v-text-field>
            </v-flex>
            <v-flex class="xs2 sm2 md2">
              <v-btn class="primary" flat
                     @click="onSearch">
                SEARCH
              </v-btn>
            </v-flex>
            <v-spacer></v-spacer>
            <v-flex class="xs12 sm12 md6">
              <v-chip small icon v-for="(item, index) in alphabet" :key="index"
                      @click="onSearchByLetter(item)">
                {{item}}
              </v-chip>
            </v-flex>
          </v-layout>
        </v-container>
      </v-card>
      <v-divider></v-divider>
    </section>

    <v-layout row wrap v-if="pSearchText">
      <v-flex xs12>
        <v-card>
          <v-flex xs12>
            <v-avatar class="warning ml-2 mt-2" title="Generic">G</v-avatar>
            Generic

            <v-dialog
              v-model="showWhatIsGeneric"
              width="500"
            >
              <a href="javascript:void()" slot="activator">
                <small>What is Generic?</small>
              </a>
              <v-card>
                <v-card-title
                  class="headline grey lighten-2"
                  primary-title
                >
                  WHAT IS A GENERIC DRUG?
                </v-card-title>

                <v-card-text>
                  <p>
                    A generic drug is a copy of the brand-name drug with the same dosage, safety, strength, quality,
                    consumption method, performance, and intended use. Before generics become available on the market,
                    the
                    generic company must prove it has the same active ingredients as the brand-name drug and works in
                    the
                    same way and in the same amount of time in the body.
                  </p>
                  <p>
                    The only differences between generics and their brand-name counterparts is that generics are less
                    expensive and may look slightly different (eg. different shape or color), as trademarks laws prevent
                    a
                    generic from looking exactly like the brand-name drug.
                  </p>
                  <p>
                    Generics are less expensive because generic manufacturers don't have to invest large sums of money
                    to
                    develop a drug. When the brand-name patent expires, generic companies can manufacture a copy of the
                    brand-name and sell it at a substantial discount.
                  </p>
                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="primary"
                    flat
                    @click="showWhatIsGeneric = false"
                  >
                    Close
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-avatar class="info ml-2 mt-2" title="Brand"><span class="text--primary text--darken-4">B</span>
            </v-avatar>
            Brand
          </v-flex>
          <v-list three-line>
            <v-subheader>
              <h4> Search result for "{{pSearchText}}". {{searchResult.length}} items has been found.</h4>
            </v-subheader>
            <template v-for="(item, index) in searchResult">
              <v-list-tile
                :key="item.id"
                avatar
                @click="">
                <v-list-tile-avatar>
                  <v-avatar v-if="item.isGeneric" class="warning" title="Generic">G</v-avatar>
                  <v-avatar v-else class="info" title="Brand"><span class="text--primary text--darken-4">B</span>
                  </v-avatar>
                </v-list-tile-avatar>

                <v-list-tile-content>
                  <v-list-tile-title>{{item.title}}</v-list-tile-title>
                  <v-list-tile-sub-title>Strength: {{ item.strength }} Quantity:{{ item.quantity }}
                  </v-list-tile-sub-title>
                  <v-list-tile-sub-title class="text--primary"><h4>Price: {{item.price | currency}}</h4>
                  </v-list-tile-sub-title>
                </v-list-tile-content>
                <v-list-tile-action>
                  <v-layout row>
                    <v-flex xs4>
                      <v-text-field label="Amount" value="1" :ref="'amount-' + item.id" mask="##" class=""
                                    validate-on-blur required
                                    :rules="[v => !!v || 'Amount is required']"></v-text-field>
                    </v-flex>
                    <v-flex mt-3 text-xs-right>
                      <v-btn icon large class="accent" title="Add To Cart" @click="addToShoppingCart(item)">
                        <v-icon>add_shopping_cart</v-icon>
                      </v-btn>
                    </v-flex>
                  </v-layout>
                </v-list-tile-action>
              </v-list-tile>
              <v-divider inset></v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
  </v-flex>
</template>
<script>
  import {mapState} from 'vuex'

  export default {
    data: () => ({
      searchText: '',
      showWhatIsGeneric: false

    }),
    props: {
      pSearchText: {
        default: null,
        required: false,
        type: String
      }
    },
    methods: {
      onSearch: function () {
        window.getApp.$emit('APP_SEARCH_DRUG', this.searchText)
      },
      onSearchByLetter: (letter) => {
        window.getApp.$emit('APP_SEARCH_DRUG', letter)
      },
      addToShoppingCart: function (product) {
        let amount = this.$refs['amount-' + product.id][0].lazyValue
        window.getApp.$emit('APP_ADD_TO_CART', {
          product: product,
          amount: amount
        })
      }
    },
    computed: {
      alphabet: () => window.getApp.alphabet,
      ...mapState({
        searchResult: state => state.products.searchResult
      })
    },
    created() {
      if (this.pSearchText) {
        this.searchText = this.pSearchText;
        this.$store.dispatch('products/find', {keyword: this.pSearchText, byLetter: this.pSearchText.length < 4})
      }
    },
    watch: {
      pSearchText: function (value) {
        if (value) {
          this.searchText = value;
          this.$store.dispatch('products/find', {keyword: value, byLetter: value.length < 4})
        }
      }
    }
  }
</script>
