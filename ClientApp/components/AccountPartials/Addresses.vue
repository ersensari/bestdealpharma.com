<template>
  <v-flex>
    <v-card>
      <v-card-title>
        <div class="headline">
          <v-icon large>location_on</v-icon>
          Shipping Addresses
        </div>
      </v-card-title>
      <v-divider></v-divider>

      <v-dialog v-model="dialog" persistent max-width="600px">
        <v-btn slot="activator" color="secondary" small flat>add new address</v-btn>
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
                  :rules="[()=>!!addressModel.addressLine || 'This field is required']"
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

      <v-container fluid grid-list-md>
        <v-layout row wrap>
          <v-flex
            v-for="item in userAddresses"
            :key="item.id"
            xs12
            sm6
            md4
            lg3
          >
            <v-card>
              <v-card-title>
                <h4>
                  {{item.addressName}}
                </h4>
                <v-spacer></v-spacer>
                <v-btn icon @click="deleteAddress(item.id)">
                  <v-icon small color="red">delete</v-icon>
                </v-btn>
              </v-card-title>
              <v-divider></v-divider>
              <v-list dense>
                <v-list-tile>
                  <v-list-tile-content>{{ item.mobilePhone }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ item.addressLine }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ item.zipCode }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ item.city }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ item.state }}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-content>{{ item.country }}</v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card>
  </v-flex>
</template>

<script>
  import {mapGetters} from "vuex";

  export default {
    data: () => ({
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
    }),
    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser', 'userAddresses']),
    },
    created() {
      this.getAddresses();
    },
    watch: {
      authenticatedUser: function (value) {
        this.$store.dispatch('user/getPersonAddresses', value.person.id);
      }
    },
    methods: {
      getAddresses() {
        if (this.authenticatedUser) {
          this.$store.dispatch('user/getPersonAddresses', this.authenticatedUser.person.id);
        }
      },
      saveAddress() {
        if (this.$refs.addressForm.validate()) {
          this.addressModel.personId = this.authenticatedUser.person.id;
          this.$store.dispatch('user/saveAddress', this.addressModel)
            .then(response => {
              this.dialog = false;
            });
        }
      },
      deleteAddress(id) {
        this.$confirm('Do you really want to delete this address?', {title: 'Warning'}).then(res => {
          if (res) {
            if (id) {
              this.$store.dispatch('user/deleteAddress', id)
                .then(response => {
                  this.getAddresses()
                });
            }
          }
        })
      }
    }
  }
</script>

