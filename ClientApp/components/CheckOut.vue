<template>
  <v-card v-if="authenticatedUser">
    <v-card-title>
      <div class="display-1">
        <v-icon large>monetization_on</v-icon>
        Check Out
      </div>
    </v-card-title>
    <v-divider></v-divider>

    <v-layout row wrap v-if="!orderFinished && cart.length>0">
      <v-flex xs12 md4 pa-3>
        <div class="headline">
          <v-icon>shopping_cart</v-icon>
          Order Information
        </div>
        <table class="v-table">
          <thead>
          <tr>
            <th class="text-xs-left">Drug Name</th>
            <th>Quantity</th>
            <th>Strength</th>
            <th>Amount</th>
            <th class="text-xs-right">Price</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="item in cart">
            <td class="text-xs-left">{{ item.product.title }}</td>
            <td class="text-xs-center">{{ item.product.quantity }}</td>
            <td class="text-xs-center">{{ item.product.strength }}</td>
            <td class="text-xs-center no-wrap">{{ item.amount }}</td>
            <td class="text-xs-right">{{item.product.price*item.amount | currency}}</td>
          </tr>
          </tbody>
          <tfoot>
          <tr>
            <td colspan="3" class="text-xs-right"><h3>Sub Total</h3></td>
            <td colspan="2" class="text-xs-right"><h3>{{calculateSubTotal() | currency}}</h3>
            </td>
          </tr>
          <tr>
            <td colspan="3" class="text-xs-right"><h3>Shipping</h3></td>
            <td colspan="2" class="text-xs-right"><h3>{{shippingPrice | currency}}</h3>
            </td>
          </tr>
          <tr>
            <td colspan="3" class="text-xs-right"><h3>Total Price</h3></td>
            <td colspan="2" class="text-xs-right"><h2 class="deep-orange--text">{{calculateSubTotal() + shippingPrice |
              currency}}</h2>
            </td>
          </tr>

          </tfoot>
        </table>
      </v-flex>
      <v-flex xs12 md8 pa-3>

        <v-stepper v-model="e1">
          <v-stepper-header>
            <v-stepper-step :complete="e1 > 1" step="1">Shipping Information</v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step :complete="e1 > 2" step="2">Upload Prescription</v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step :complete="e1 > 3" step="3">Payment & Finish</v-stepper-step>

          </v-stepper-header>

          <v-stepper-items>
            <v-stepper-content step="1">
              <v-card class="mb-5 elevation-0">
                <v-subheader>Select a shipping address from your address book or enter a
                  <v-dialog v-model="dialog" persistent max-width="600px">
                    <v-btn slot="activator" color="secondary" small flat>new address</v-btn>
                    <v-card>
                      <v-card-title>
                        <span class="headline">Add New Address</span>
                      </v-card-title>
                      <v-card-text>
                        <v-form ref="addressForm" wrap>
                          <v-flex xs12>
                            <v-text-field label="Address Name"
                                          :rules="[() => !!addressModel.addressName || 'This field is required']"
                                          v-model="addressModel.addressName"></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field label="Phone Number"
                                          :rules="[() => !!addressModel.mobilePhone || 'This field is required']"
                                          v-model="addressModel.mobilePhone"></v-text-field>
                          </v-flex>
                          <v-flex xs12>

                            <v-text-field
                              :rules="[
                                          ()=>
                                          !!addressModel.addressLine || 'This field is required']"
                              v-model="addressModel.addressLine"
                              label="Address Line"
                              counter="25"
                              required>
                            </v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              :rules="[() => !!addressModel.city || 'This field is required']"
                              v-model="addressModel.city"
                              label="City"
                              required></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              v-model="addressModel.state"
                              :rules="[() => !!addressModel.state || 'This field is required']"
                              label="State/Province/Region"
                              required></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              :rules="[() => !!addressModel.zipCode || 'This field is required']"
                              v-model="addressModel.zipCode"
                              label="ZIP / Postal Code"
                              required></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-autocomplete
                              :rules="[() => !!addressModel.country || 'This field is required']"
                              :items="countries"
                              v-model="addressModel.country"
                              label="Country"
                              placeholder="Select..."
                              required></v-autocomplete>
                          </v-flex>
                        </v-form>
                      </v-card-text>
                      <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="dialog = false">Close</v-btn>
                        <v-btn color="blue darken-1" flat @click="saveAddress">Save</v-btn>
                      </v-card-actions>
                    </v-card>
                  </v-dialog>
                </v-subheader>

                <v-container fluid grid-list-md>
                  <v-data-iterator
                    :items="allAddresses"
                    content-tag="v-layout"
                    hide-actions
                    row
                    wrap
                  >
                    <v-flex
                      slot="item"
                      slot-scope="props"
                      xs12
                      sm6
                      md4
                      lg3
                    >
                      <v-card
                        :class="{'primary':selectedAddress!=null && selectedAddress.addressName===props.item.addressName}">
                        <v-card-title class="pa-0 ma-0">
                          <h4>
                            <v-checkbox :label="props.item.addressName"
                                        :value="props.item" v-model="selectedAddress"></v-checkbox>
                          </h4>
                        </v-card-title>
                        <v-divider></v-divider>
                        <v-list dense>
                          <v-list-tile>
                            <v-list-tile-content>{{ props.item.mobilePhone }}</v-list-tile-content>
                          </v-list-tile>
                          <v-list-tile>
                            <v-list-tile-content>{{ props.item.addressLine }}</v-list-tile-content>
                          </v-list-tile>
                          <v-list-tile>
                            <v-list-tile-content>{{ props.item.zipCode }}</v-list-tile-content>
                          </v-list-tile>
                          <v-list-tile>
                            <v-list-tile-content>{{ props.item.city }}</v-list-tile-content>
                          </v-list-tile>
                          <v-list-tile>
                            <v-list-tile-content>{{ props.item.state }}</v-list-tile-content>
                          </v-list-tile>
                          <v-list-tile>
                            <v-list-tile-content>{{ props.item.country }}</v-list-tile-content>
                          </v-list-tile>
                        </v-list>
                      </v-card>
                    </v-flex>
                  </v-data-iterator>
                </v-container>
              </v-card>

              <v-btn
                color="primary"
                :disabled="selectedAddress==null"
                @click="e1=2"
              >
                Continue
              </v-btn>
            </v-stepper-content>

            <v-stepper-content step="2">
              <v-card class="mb-5 elevation-0">
                <h3>Please upload your prescription file.</h3>
                <v-alert type="success" v-model="fileUploadSuccess">Upload successful.</v-alert>
                <file-upload url='/api/Order/upload' @success="onFileUploadSucess"></file-upload>
                <v-flex mt-5>
                  <v-autocomplete
                    :rules="[() => !!selectedPrescription || 'This field is required']"
                    :items="prescriptions"
                    v-model="selectedPrescription"
                    item-text="name"
                    item-value="id"
                    label="Please select your prescription after upload"></v-autocomplete>

                </v-flex>
              </v-card>

              <v-btn
                color="primary"
                @click="e1 = 3"
                :disabled="selectedPrescription==null"
              >
                Continue
              </v-btn>

              <v-btn flat @click="e1=1">Back</v-btn>
            </v-stepper-content>

            <v-stepper-content step="3">
              <v-card
                class="mb-5 elevation-0"
              >
                <v-layout
                  class="grid-list-md wrap"
                >
                  <v-flex text-center-xs xs12>
                    <div class="display-1">Thank you for choosing us!</div>
                    <v-textarea v-model="customerExplanation" label="Order Explanation"></v-textarea>
                  </v-flex>
                  <v-flex text-xs-center xs12>
                    <p>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ullamcorper vel diam eu vulputate.
                      Aliquam vestibulum felis sit amet tincidunt consectetur. Aenean tristique nulla at nisl mattis
                      varius. Vivamus tempus dui vitae libero rhoncus, vel cursus quam congue. Nam mattis neque et
                      augue ullamcorper viverra.
                    </p>
                  </v-flex>
                  <v-flex text-xs-center xs12 mt-5 v-if="!orderCompleting">
                    <pay-pal
                      :amount="calculateTotal()"
                      currency="USD"
                      locale="en_US"
                      :client="paypal"
                      :button-style="aStyle"
                      v-on:payment-completed="createOrder(1)"
                      env="sandbox">
                    </pay-pal>

                  </v-flex>
                  <v-flex text-xs-center xs12 mt-5 class="deep-orange--text" v-else>
                    <h3>Order completing please wait...</h3>
                  </v-flex>
                </v-layout>
              </v-card>
            </v-stepper-content>

          </v-stepper-items>
        </v-stepper>
      </v-flex>
    </v-layout>

    <v-container fluid fill-height v-else-if="!orderFinished && cart.length === 0">
      <v-layout
        justify-center
        align-center
        fill-height>
        <v-flex text-xs-center>
          <div class="display-1 mb-5">Your shopping cart is empty!</div>
          <v-btn large class="deep-orange" dark to="/new-order">Create New Order</v-btn>
        </v-flex>
      </v-layout>
    </v-container>


    <v-container fluid fill-height v-else-if="orderFinished">
      <v-layout
        justify-center
        align-center
        fill-height
        grid-list-md
        row
      >
        <v-flex text-xs-center>
          <div class="display-1">Thank you for choosing us!</div>
          <v-subheader>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ullamcorper vel diam eu vulputate.
            Aliquam vestibulum felis sit amet tincidunt consectetur. Aenean tristique nulla at nisl mattis
            varius. Vivamus tempus dui vitae libero rhoncus, vel cursus quam congue. Nam mattis neque et
            augue ullamcorper viverra.
          </v-subheader>
          <v-btn large class="deep-orange" dark to="/new-order">Create New Order</v-btn>
          <br/>
          <br/>
        </v-flex>
      </v-layout>
    </v-container>
  </v-card>
</template>

<script>

  import {mapGetters, mapState} from "vuex";
  import FileUpload from 'v-file-upload'
  import moment from 'moment'
  import PayPal from 'vue-paypal-checkout'

  export default {
    components: {
      'file-upload': FileUpload,
      'pay-pal': PayPal
    },

    data: () => ({
      aStyle: {
        label: 'pay',
        size: 'large',
        shape: 'rect',
        color: 'blue',
      },
      paypal: {
        sandbox: 'ATyC7QwwTes-xkqtDC5_Bsue03B0YCqaDyChGB7YWq_Z-H_fdmY-7sJ-52F-5wAI-WRZZqz2ayW6HDr-',
        production: 'todo:alinacak'
      },
      orderCompleting: false,
      e1: 0,
      countries: ['Afghanistan', 'Albania', 'Algeria', 'Andorra', 'Angola', 'Anguilla', 'Antigua &amp; Barbuda', 'Argentina', 'Armenia', 'Aruba', 'Australia', 'Austria', 'Azerbaijan', 'Bahamas', 'Bahrain', 'Bangladesh', 'Barbados', 'Belarus', 'Belgium', 'Belize', 'Benin', 'Bermuda', 'Bhutan', 'Bolivia', 'Bosnia Herzegovina', 'Botswana', 'Brazil', 'British Virgin Islands', 'Brunei', 'Bulgaria', 'Burkina Faso', 'Burundi', 'Cambodia', 'Cameroon', 'Cape Verde', 'Cayman Islands', 'Chad', 'Chile', 'China', 'Colombia', 'Congo', 'Cook Islands', 'Costa Rica', 'Cote D Ivoire', 'Croatia', 'Cruise Ship', 'Cuba', 'Cyprus', 'Czech Republic', 'Denmark', 'Djibouti', 'Dominica', 'Dominican Republic', 'Ecuador', 'Egypt', 'El Salvador', 'Equatorial Guinea', 'Estonia', 'Ethiopia', 'Falkland Islands', 'Faroe Islands', 'Fiji', 'Finland', 'France', 'French Polynesia', 'French West Indies', 'Gabon', 'Gambia', 'Georgia', 'Germany', 'Ghana', 'Gibraltar', 'Greece', 'Greenland', 'Grenada', 'Guam', 'Guatemala', 'Guernsey', 'Guinea', 'Guinea Bissau', 'Guyana', 'Haiti', 'Honduras', 'Hong Kong', 'Hungary', 'Iceland', 'India', 'Indonesia', 'Iran', 'Iraq', 'Ireland', 'Isle of Man', 'Israel', 'Italy', 'Jamaica', 'Japan', 'Jersey', 'Jordan', 'Kazakhstan', 'Kenya', 'Kuwait', 'Kyrgyz Republic', 'Laos', 'Latvia', 'Lebanon', 'Lesotho', 'Liberia', 'Libya', 'Liechtenstein', 'Lithuania', 'Luxembourg', 'Macau', 'Macedonia', 'Madagascar', 'Malawi', 'Malaysia', 'Maldives', 'Mali', 'Malta', 'Mauritania', 'Mauritius', 'Mexico', 'Moldova', 'Monaco', 'Mongolia', 'Montenegro', 'Montserrat', 'Morocco', 'Mozambique', 'Namibia', 'Nepal', 'Netherlands', 'Netherlands Antilles', 'New Caledonia', 'New Zealand', 'Nicaragua', 'Niger', 'Nigeria', 'Norway', 'Oman', 'Pakistan', 'Palestine', 'Panama', 'Papua New Guinea', 'Paraguay', 'Peru', 'Philippines', 'Poland', 'Portugal', 'Puerto Rico', 'Qatar', 'Reunion', 'Romania', 'Russia', 'Rwanda', 'Saint Pierre &amp; Miquelon', 'Samoa', 'San Marino', 'Satellite', 'Saudi Arabia', 'Senegal', 'Serbia', 'Seychelles', 'Sierra Leone', 'Singapore', 'Slovakia', 'Slovenia', 'South Africa', 'South Korea', 'Spain', 'Sri Lanka', 'St Kitts &amp; Nevis', 'St Lucia', 'St Vincent', 'St. Lucia', 'Sudan', 'Suriname', 'Swaziland', 'Sweden', 'Switzerland', 'Syria', 'Taiwan', 'Tajikistan', 'Tanzania', 'Thailand', "Timor L'Este", 'Togo', 'Tonga', 'Trinidad &amp; Tobago', 'Tunisia', 'Turkey', 'Turkmenistan', 'Turks &amp; Caicos', 'Uganda', 'Ukraine', 'United Arab Emirates', 'United Kingdom', 'United States', 'Uruguay', 'Uzbekistan', 'Venezuela', 'Vietnam', 'Virgin Islands (US)', 'Yemen', 'Zambia', 'Zimbabwe'],
      addressModel: {
        id: 0,
        addressName: null,
        mobilePhone: null,
        addressLine: null,
        city: null,
        state: null,
        zipCode: null,
        country: null,
        personId: 0
      },
      dialog: false,
      cart: [],
      shippingPrice: null,
      shippingMethod: null,
      selectedAddress: null,
      selectedPrescription: null,
      fileUploadSuccess: false,
      orderFinished: false,
      customerExplanation: '',
      allAddresses: []
    }),
    created() {
      if (!this.$route.params.shippingId) {
        this.$router.push({name: 'shopping-cart'});
      }
      this.cart = window.getApp.cart;
      this.initializePage()
    },
    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser', 'userAddresses']),
      ...mapState({
        prescriptions: state => state.orders.prescriptions,
        shippingItems: state => state.shippings.all
      })
    },

    watch: {
      authenticatedUser: function (value) {
        this.initializePage()
      },
      shippingItems: function (value) {
        const shipping = value.find(x => x.id === this.$route.params.shippingId);
        this.shippingPrice = shipping.price;
        this.shippingMethod = shipping;
      }
    },

    methods: {
      initializePage() {
        if (this.authenticatedUser) {
          this.$store.dispatch('user/getPersonAddresses', this.authenticatedUser.person.id).then(response => {
            this.allAddresses = response.data;
          });
          this.$store.dispatch('orders/getPrescriptions');
          this.$store.dispatch('shippings/getAll');
        }
      },
      calculateSubTotal() {
        return _.sumBy(this.cart, function (i) {
          return i.product.price * i.amount
        })
      },
      calculateTotal() {
        const subTotal = this.calculateSubTotal();
        return (subTotal + this.shippingPrice).toString();
      },
      saveAddress() {
        if (this.$refs.addressForm.validate()) {
          this.selectedAddress = null;
          this.addressModel.personId = this.authenticatedUser.person.id;
          this.$store.dispatch('user/saveAddress', this.addressModel)
            .then(response => {
              this.dialog = false;
              this.initializePage();
            });
        }
      },
      onFileUploadSucess() {
        this.$store.dispatch('orders/getPrescriptions').then(response => {
          if (response.data.length > 0) {
            this.fileUploadSuccess = true
          }
        })
      },
      createOrder(status) {
        this.orderCompleting = true;
        const model = {
          id: 0,
          orderNumber: moment().valueOf().toString(),
          orderDate: moment().format('YYYY-MM-DD HH:mm'),
          mobilePhone: this.selectedAddress.mobilePhone,
          zipCode: this.selectedAddress.zipCode,
          state: this.selectedAddress.state,
          city: this.selectedAddress.city,
          country: this.selectedAddress.country,
          addressLine: this.selectedAddress.addressLine,
          customerExplanation: this.customerExplanation,
          subTotal: _.sumBy(this.cart, function (i) {
            return i.product.price * i.amount
          }),
          shipping: this.shippingMethod.price,
          shippingMethod: this.shippingMethod.text,
          total: _.sumBy(this.cart, function (i) {
            return i.product.price * i.amount
          }) + this.shippingMethod.price,
          status: status,
          orderDetails: this.cart.map(x => {
            return {
              id: 0,
              title: x.product.title,
              manufacturer: x.product.manufacturer,
              strength: x.product.strength,
              quantity: x.product.quantity,
              price: x.product.price,
              isGeneric: x.product.isGeneric,
              onlyRx: x.product.onlyRx,
              isAvailable: x.product.isAvailable,
              amount: x.amount,
              totalPrice: x.amount * x.product.price
            }
          }),
          personId: this.authenticatedUser.person.id,
          prescriptionId: this.selectedPrescription
        };

        this.$store.dispatch('orders/createOrder', model).then(response => {
          this.orderFinished = true;
          window.getApp.$emit('APP_CLEAR_CART');
        });

        // const paypallnk = "https://www.paypal.com/cgi-bin/webscr?cmd=_ext-enter&redirect_cmd=_xclick&first_name="
        //   + this.authenticatedUser.person.name
        //   + "&last_name="
        //   + this.authenticatedUser.surname
        //   + "&business=accounting@amerikadanhemenal.com&amount="
        //   + model.total
        //   + "&currency_code=USD&item_name=Odeme&item_number="
        //   + model.orderNumber
        //   + "&quantity="
        //   + this.cart.length
        //   + "&item_name=Payment for bestdealpharma.com #" + model.orderNumber
        //   + "&shipping="
        //   + model.shipping
        //   + "&no_shipping=0&pbtype=product";


      }
    }
  }
</script>

<style scoped>

</style>
