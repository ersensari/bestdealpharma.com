<template>
  <v-flex>
    <section>
      <v-parallax src="/images/sections/register.jpg" height="300">
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <v-icon x-large class="deep-orange--text">person_add</v-icon>
          <h1 class="blue--text text--darken-4 mb-2 display-1 text-xs-center">New Customer Registration</h1>
          <div class="blue--text text--darken-4 subheading mb-3 text-xs-center deep-orange--text">
            BestDealPharma.com is
            your life insurance
          </div>
        </v-layout>
      </v-parallax>
    </section>
    <section>
      <v-container fluid grid-list-md>
        <v-layout row wrap>
          <v-flex xs12 sm12 md7 lg6>
            <v-card class="elevation-1">
              <v-card-text>
                <div class="layout column align-center">
                  <v-icon large class="deep-orange--text">person_add</v-icon>
                  <h1 class="flex my-4 primary--text">New Customer Registration</h1>
                  <h3>BestDealPharma.com is your life insurance</h3>
                </div>
                <v-form ref="form">
                  <v-text-field ref="email"
                                v-model="email"
                                :rules="[rules.required, rules.email]"
                                label="E-mail"></v-text-field>
                  <v-text-field ref="password"
                                v-model="password"
                                :append-icon="showpwd1 ? 'visibility_off' : 'visibility'"
                                :rules="[rules.required, rules.min]"
                                :type="showpwd1 ? 'text' : 'password'"
                                label="Password"
                                hint="At least 6 characters"
                                counter
                                @click:append="showpwd1 = !showpwd1"></v-text-field>
                  <v-text-field ref="confirmpassword"
                                v-model="confirmpassword"
                                :append-icon="showpwd2 ? 'visibility_off' : 'visibility'"
                                :rules="[rules.required, (v) => v == this.password || 'The passwords you entered don\'t match']"
                                :type="showpwd2 ? 'text' : 'password'"
                                label="Confirm Password"
                                counter
                                @click:append="showpwd2 = !showpwd2"></v-text-field>
                  <v-text-field ref="name"
                                v-model="name"
                                :rules="[() => !!name || 'This field is required']"
                                label="Name"
                                required></v-text-field>
                  <v-text-field ref="surname"
                                v-model="surname"
                                :rules="[() => !!surname || 'This field is required']"
                                label="Lastname"
                                required></v-text-field>
                  <v-menu :close-on-content-click="false"
                          v-model="birthdatepicker"
                          :nudge-right="40"
                          lazy
                          transition="scale-transition"
                          offset-y
                          full-width
                          max-width="290px"
                          min-width="290px"
                          ref="birthdatepickermenu">
                    <v-text-field ref="birthdate"
                                  slot="activator"
                                  v-model="birthdate"
                                  label="Date Of Birth"
                                  readonly
                                  :rules="[() => !!birthdate || 'This field is required']"></v-text-field>
                    <v-date-picker v-model="birthdate"
                                   :max="new Date().toISOString().substr(0, 10)"
                                   min="1935-01-01"
                                   ref="birthdatepicker"></v-date-picker>
                  </v-menu>
                  <v-text-field ref="mobilephone"
                                v-model="mobilephone"
                                :rules="[() => !!mobilephone || 'This field is required']"
                                label="Mobile Phone"
                                required></v-text-field>
                  <v-text-field ref="homephone"
                                v-model="homephone"
                                label="Home Phone"
                                hint="Optional"></v-text-field>
                  <v-text-field ref="address"
                                :rules="[()=>!!address || 'This field is required']"
                                v-model="address"
                                label="Address Line"
                                counter="25"
                                required
                  >
                  </v-text-field>
                  <v-text-field ref="city"
                                :rules="[() => !!city || 'This field is required']"
                                v-model="city"
                                label="City"
                                required></v-text-field>
                  <v-text-field ref="state"
                                v-model="state"
                                :rules="[() => !!state || 'This field is required']"
                                label="State/Province/Region"
                                required></v-text-field>
                  <v-text-field ref="zipcode"
                                :rules="[() => !!zipcode || 'This field is required']"
                                v-model="zipcode"
                                label="ZIP / Postal Code"
                                required></v-text-field>
                  <v-autocomplete ref="country"
                                  :rules="[() => !!country || 'This field is required']"
                                  :items="countries"
                                  v-model="country"
                                  label="Country"
                                  placeholder="Select..."
                                  required></v-autocomplete>
                  <vue-perfect-scrollbar class="drawer-menu--scroll" :settings="scrollSettings">
                    <v-flex class="agreement-scroll-area">
                      <h1>PATIENT AUTHORIZATION AGREEMENT</h1>
                      <p>
                        Patient Authorization Agreement
                        BestPrice Rx which includes its officers, directors, affiliates, representatives, agents,
                        contractors
                        and sub-contractors (collectively, "BestPrice Rx") is an international prescription referral
                        service
                        committed to helping ensure that I, the undersigned patient ("I" or "Me"), am able to obtain
                        medication, products and /or services ("Product") from licensed pharmacies. This Patient
                        Authorization
                        Agreement ("Agreement") shall govern all sales of Product facilitated by BestPrice Rx between me
                        and
                        any of BestPrice Rx's authorized pharmacies located in Canada, the United Kingdom, India,
                        Mauritius,
                        Turkey, Vanuatu, USA, and elsewhere (collectively, the "Pharmacy"). I acknowledge and agree as
                        follows:
                      </p>
                      <p>
                        1. I am the age of majority, am fully competent to make my own health care decisions and have
                        obtained
                        any prescription(s) for the Product in a lawful manner.
                      </p>
                      <p>
                        2. I must have been taking the prescribed medication for a minimum period of thirty (30) days
                        immediately prior to the date that I submit my prescription to BestPrice Rx for filling.
                      </p>
                      <p>
                        3. I understand that it is my responsibility to have my prescribing physician ("My Own
                        Physician")
                        conduct regular physical examinations, including any and all suggested testing to ensure that I
                        have
                        no medical problems which would constitute a contraindication to me taking the Product. I
                        certify
                        that
                        I have had a physical examination by My Own Physician within the last two (2) months from the
                        date
                        hereof.
                      </p>
                      <p>
                        4. I agree that if I suffer any adverse effects while taking any prescription medication that I
                        will
                        immediately contact My Own Physician and that in the event that I come under the care of another
                        physician, I will inform him or her of any and all medications that I have been prescribed. I
                        further
                        acknowledge and agree that BestPrice Rx recommends regular physician examinations with My Own
                        Physician whose care I am under and who initially prescribed my medications.
                      </p>
                      <p>
                        5. I agree and understand that it would be a violation of the law to falsify any information
                        provided
                        to BestPrice Rx, including, but not limited to, any information on the BestPrice Rx Order Form
                        ("Order
                        Form"). I agree to truthfully, and to the best of my knowledge, answer all of the questions on
                        the
                        Order Form. I further agree and understand that I will be solely responsible for any adverse
                        effects
                        that I may suffer from taking or continuing to take the Product in the event that I have failed
                        to
                        fully furnish my complete and accurate medical history and/or if I fail to notify My Own
                        Physician
                        and
                        BestPrice Rx of any change in my physical or medical condition.
                      </p>
                      <p>
                        6. I further understand that BestPrice Rx will only verify and provide Product that My Own
                        Physician
                        has already prescribed to me. No new prescription medications will be provided by BestPrice Rx.
                        I
                        also
                        understand that no controlled medications, narcotics, tranquilizers, or other medications that
                        BestPrice Rx decides are inappropriate, will be provided.
                      </p>
                      <p>
                        7. I appoint BestPrice Rx to act as my agent and attorney in order to take all steps that it
                        deems
                        necessary to have the Product dispensed by the Pharmacy, to the same extent as I could do if I
                        were
                        personally present at the Pharmacy, including: (a) collecting personal health information about
                        me;
                        (b) disclosing that information to and having a licenced physician perform an independent
                        medical
                        review in order to obtain a valid prescription for the Product; and (c) packaging the Product
                        and
                        delivering it to me. I hereby waive any requirement of the physician to conduct a physical
                        examination. This authorization may be revoked by me at any time and shall continue until such
                        revocation has been provided to BestPrice Rx, in writing.
                      </p>
                      <p>
                        8. There will be no additional fees charged to me in the event that an independent medical
                        review
                        is
                        required to obtain a valid prescription for the Product.
                      </p>
                      <p>
                        9. I initiated contact with and understand that BestPrice Rx is not located in the United
                        States.
                      </p>
                      <p>
                        10. The Product is sold and dispensed by the Pharmacy in accordance with the laws of the
                        jurisdiction
                        in which the Pharmacy is located. Title to the Product passes from the Pharmacy to me when the
                        Product
                        leaves the Pharmacy.
                      </p>
                      <p>
                        11. Any and all physicians and/or pharmacists ("Providers") retained by BestPrice Rx in order to
                        obtain the Product from the Pharmacy are located and licensed to practice in the jurisdiction in
                        which
                        they are located. Any treatment that I receive from the Providers shall be deemed to be received
                        by
                        me
                        in the jurisdiction in which the Providers are located.
                      </p>
                      <p>
                        12. I understand and agree that the review of my medical information by a physician is in no way
                        intended as a means to diagnose any medical condition and does not substitute the requirement
                        for
                        me
                        to obtain my own professional medical advice from My Own Physician. I agree to a direct all
                        questions
                        to My Own Physician. I will consult My Own Physician before taking any new drug or changing my
                        daily
                        health regimen.
                      </p>
                      <p>
                        13. Any and all agreements reached or contracts formed and transactions undertaken with or
                        involving
                        the Pharmacy are and shall be deemed to be made in the jurisdiction of the Pharmacy and shall be
                        governed by the laws of the jurisdiction of the Pharmacy applicable to such contracts,
                        agreements
                        and
                        transactions(unless BestPrice Rx elects otherwise in its sole discretion) . The Courts of that
                        jurisdiction shall have sole and exclusive jurisdiction over any dispute that may arise between
                        me
                        and
                        the Pharmacy and I agree to attorn to the Courts of that jurisdiction for any and all such
                        dispute
                        or
                        disputes (unless BestPrice Rx elects otherwise in its sole discretion).
                      </p>
                      <p>
                        14. BestPrice Rx may communicate with me via email or telephone to discuss my order or pending
                        refill
                        order for the Product.
                      </p>
                      <p>
                        15. Not all of the services or products shown on BestPrice Rx's website are available in all
                        jurisdictions.
                      </p>
                      <p>
                        16. Your credit card company may charge you a foreign transaction fee at their discretion which
                        is
                        in
                        addition to the amount charged by BestPrice Rx. Foreign transaction fees are charged by the
                        customers'
                        card issuer and not by BestPrice Rx.
                      </p>
                      <p>
                        17. I acknowledge that the terms and conditions as found in this Agreement are readily available
                        to
                        me
                        on a 24-hour basis from BestPrice Rx's website and acknowledge having had every opportunity to
                        obtain
                        independent legal advice with respect to this Agreement.
                      </p>
                      <p>
                        I HAVE READ AND UNDERSTAND THE FORGOING TERMS AND I AGREE THAT THEY SHALL BE BINDING UPON ME AND
                        MY
                        HEIRS, ASSIGNS, SUCCESSORS AND PERSONAL REPRESENTATIVES.

                        OR

                        "I am the parent/legal guardian/power of attorney for the customer disclosed herein, am over the
                        age
                        of majority, and have full authority to sign for and provide the above representations to
                        BestPrice
                        Rx
                        on the customer's behalf."
                      </p>
                    </v-flex>
                  </vue-perfect-scrollbar>
                  <v-checkbox v-model="agreement"
                              :rules="[v => !!v || 'You must agree to continue!']"
                              label="Yes, I agree to the Patient Authorization Agreement"
                              required></v-checkbox>
                </v-form>
              </v-card-text>
              <v-divider class="mt-5"></v-divider>
              <v-card-actions>
                <v-flex class="text-xs-center">
                  <v-btn color="primary" class="x-large" @click="register">Register</v-btn>
                </v-flex>
              </v-card-actions>

              <v-list color="error transparent" v-model="showRegisterError" transition="scale-transition">
                <v-list-tile v-for="err in errorMessages" :key="err.name">
                  <v-list-tile-action>
                    <v-icon color="error">
                      error
                    </v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title v-text="err.description" class="deep-orange--text"></v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-card>
          </v-flex>
          <v-flex xs12 sm12 md5 lg6>
            <v-card class="elevation-1">
              <v-card-text>
                <div class="layout column align-center">
                  <v-icon large class="deep-orange--text">https</v-icon>
                  <h1 class="flex my-4 primary--text">Existing Customers</h1>
                  <h3>If you have an account with us, please log in.</h3>
                </div>
                <v-form lazy-validation v-model="loginFormIsValid" ref="loginForm">
                  <v-text-field append-icon="person" label="Email" type="text" v-model="loginModel.email" required
                                @keyup.native.enter="login"
                                :rules="[rules.required, rules.email]">
                  </v-text-field>
                  <v-text-field append-icon="lock" label="Password" type="password" v-model="loginModel.password"
                                required
                                :rules="[v => !!v || 'Password is required']" @keyup.native.enter="login">
                  </v-text-field>
                </v-form>
                <v-alert type="error" dismissible v-model="showLoginError" transition="scale-transition">
                  Opps! Email or Password is wrong!
                </v-alert>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="login" :loading="loginLoading">Login</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </section>
  </v-flex>
</template>
<script>
  import VuePerfectScrollbar from 'vue-perfect-scrollbar'

  export default {
    components: {
      VuePerfectScrollbar
    },
    data: () => ({
      scrollSettings: {
        maxScrollbarLength: 160
      },
      countries: ['Afghanistan', 'Albania', 'Algeria', 'Andorra', 'Angola', 'Anguilla', 'Antigua &amp; Barbuda', 'Argentina', 'Armenia', 'Aruba', 'Australia', 'Austria', 'Azerbaijan', 'Bahamas', 'Bahrain', 'Bangladesh', 'Barbados', 'Belarus', 'Belgium', 'Belize', 'Benin', 'Bermuda', 'Bhutan', 'Bolivia', 'Bosnia &amp; Herzegovina', 'Botswana', 'Brazil', 'British Virgin Islands', 'Brunei', 'Bulgaria', 'Burkina Faso', 'Burundi', 'Cambodia', 'Cameroon', 'Cape Verde', 'Cayman Islands', 'Chad', 'Chile', 'China', 'Colombia', 'Congo', 'Cook Islands', 'Costa Rica', 'Cote D Ivoire', 'Croatia', 'Cruise Ship', 'Cuba', 'Cyprus', 'Czech Republic', 'Denmark', 'Djibouti', 'Dominica', 'Dominican Republic', 'Ecuador', 'Egypt', 'El Salvador', 'Equatorial Guinea', 'Estonia', 'Ethiopia', 'Falkland Islands', 'Faroe Islands', 'Fiji', 'Finland', 'France', 'French Polynesia', 'French West Indies', 'Gabon', 'Gambia', 'Georgia', 'Germany', 'Ghana', 'Gibraltar', 'Greece', 'Greenland', 'Grenada', 'Guam', 'Guatemala', 'Guernsey', 'Guinea', 'Guinea Bissau', 'Guyana', 'Haiti', 'Honduras', 'Hong Kong', 'Hungary', 'Iceland', 'India', 'Indonesia', 'Iran', 'Iraq', 'Ireland', 'Isle of Man', 'Israel', 'Italy', 'Jamaica', 'Japan', 'Jersey', 'Jordan', 'Kazakhstan', 'Kenya', 'Kuwait', 'Kyrgyz Republic', 'Laos', 'Latvia', 'Lebanon', 'Lesotho', 'Liberia', 'Libya', 'Liechtenstein', 'Lithuania', 'Luxembourg', 'Macau', 'Macedonia', 'Madagascar', 'Malawi', 'Malaysia', 'Maldives', 'Mali', 'Malta', 'Mauritania', 'Mauritius', 'Mexico', 'Moldova', 'Monaco', 'Mongolia', 'Montenegro', 'Montserrat', 'Morocco', 'Mozambique', 'Namibia', 'Nepal', 'Netherlands', 'Netherlands Antilles', 'New Caledonia', 'New Zealand', 'Nicaragua', 'Niger', 'Nigeria', 'Norway', 'Oman', 'Pakistan', 'Palestine', 'Panama', 'Papua New Guinea', 'Paraguay', 'Peru', 'Philippines', 'Poland', 'Portugal', 'Puerto Rico', 'Qatar', 'Reunion', 'Romania', 'Russia', 'Rwanda', 'Saint Pierre &amp; Miquelon', 'Samoa', 'San Marino', 'Satellite', 'Saudi Arabia', 'Senegal', 'Serbia', 'Seychelles', 'Sierra Leone', 'Singapore', 'Slovakia', 'Slovenia', 'South Africa', 'South Korea', 'Spain', 'Sri Lanka', 'St Kitts &amp; Nevis', 'St Lucia', 'St Vincent', 'St. Lucia', 'Sudan', 'Suriname', 'Swaziland', 'Sweden', 'Switzerland', 'Syria', 'Taiwan', 'Tajikistan', 'Tanzania', 'Thailand', "Timor L'Este", 'Togo', 'Tonga', 'Trinidad &amp; Tobago', 'Tunisia', 'Turkey', 'Turkmenistan', 'Turks &amp; Caicos', 'Uganda', 'Ukraine', 'United Arab Emirates', 'United Kingdom', 'United States', 'Uruguay', 'Uzbekistan', 'Venezuela', 'Vietnam', 'Virgin Islands (US)', 'Yemen', 'Zambia', 'Zimbabwe'],
      errorMessages: [],
      email: null,
      name: null,
      surname: null,
      mobilephone: null,
      homephone: null,
      birthdate: null,
      address: null,
      city: null,
      state: null,
      zipcode: null,
      country: null,
      password: null,
      showpwd1: false,
      showpwd2: false,
      confirmpassword: null,
      birthdatepicker: false,
      agreement: false,
      rules: {
        required: value => !!value || 'This field is required.',
        min: v => v && v.length >= 6 || 'Min 6 characters',
        passwordMatch: (v) => v == this.password || 'The passwords you entered don\'t match',
        email: value => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || 'E-mail must be valid.'
        }
      },
      showRegisterError: false,
      showLoginError: false,
      loginFormIsValid: false,
      loginLoading: false,
      loginModel: {
        email: null,
        password: null,
        forAdminPanel: false
      }

    }),
    watch: {
      birthdatepicker(val) {
        val && this.$nextTick(() => (this.$refs.birthdatepicker.activePicker = 'YEAR'))
      }
    },
    computed: {
      form() {
        return {
          email: this.email,
          name: this.name,
          surname: this.surname,
          mobilePhone: this.mobilephone,
          homePhone: this.homephone,
          birthDate: this.birthdate,
          address: this.address,
          city: this.city,
          state: this.state,
          zipCode: this.zipcode,
          country: this.country,
          password: this.password
        }
      }
    },

    methods: {
      register() {
        this.errorMessages = '';
        this.showRegisterError = false;
        if (this.$refs.form.validate()) {
          this.axios.post('/account/register', JSON.stringify(this.form), {
            headers: {
              'Content-Type': 'application/json'
            }
          }).then(
            (response) => {
              this.$myLocalStorage.setEnc('user', response.data.user);
              window.localStorage.setItem('token', response.data.token);
              this.$store.dispatch('user/getAuthenticatedUser');

              this.$router.push(response.data.returnUrl)
            }
          ).catch(err => {
            if (err.response.status === 401) {
              this.showRegisterError = true
            } else if (err.response.status === 400) {
              this.showRegisterError = true;
              this.errorMessages = err.response.data;
            } else {
              console.log(err)
            }
          })
        }
      },

      login() {
        if (this.$refs.loginForm.validate()) {
          this.showLoginError = false;
          this.axios.post('/account/login', JSON.stringify(this.loginModel), {
            headers: {
              'Content-Type': 'application/json'
            }
          }).then(
            (response) => {
              this.$myLocalStorage.setEnc('user', response.data.user);
              window.localStorage.setItem('token', response.data.token);
              this.$store.dispatch('user/getAuthenticatedUser');

              this.$router.push(response.data.returnUrl)
            }
          ).catch(err => {
            if (err.response.status === 401) {
              this.showLoginError = true
            } else {
              console.log(err)
            }
          })
        }
      }
    }
  }
</script>
<style lang="scss">

  .agreement-scroll-area {
    position: relative;
    margin: auto;
    height: 300px;
  }
</style>
