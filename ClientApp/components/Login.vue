<template>
  <v-flex>
    <section>
      <v-parallax src="/images/sections/register.jpg" height="300">
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <v-icon x-large class="deep-orange--text">lock</v-icon>
          <h1 class="blue--text text--darken-4 mb-2 display-1 text-xs-center">Login</h1>
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
          <v-flex xs12 sm12 md5 lg6>
            <v-card class="elevation-1">
              <v-card-text>
                <div class="layout column align-center">
                  <v-icon large class="deep-orange--text">https</v-icon>
                  <h1 class="flex my-4 primary--text">Existing Customers</h1>
                  <h3>If you have an account with us, please log in.</h3>
                </div>
                <v-form lazy-validation v-model="loginFormIsValid" ref="loginForm">
                  <v-text-field append-icon="person" label="Email" type="text" v-model="loginModel.email"
                                @keyup.native.enter="login"
                                :rules="[rules.required,rules.email]">
                  </v-text-field>
                  <v-text-field append-icon="lock" label="Password" type="password" v-model="loginModel.password"
                                :rules="[rules.required]" @keyup.native.enter="login">
                  </v-text-field>
                </v-form>
                <v-alert type="error" dismissible v-model="showLoginError" transition="scale-transition">
                  Opps! Email or Password is wrong!
                </v-alert>
              </v-card-text>
              <v-card-actions>
                <a href="/rescue-password">Did you forget your password?</a>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="login" :loading="loginLoading">Login</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>

          <v-flex xs12 sm12 md7 lg6>
            <v-card class="elevation-1">
              <v-card-text>
                <div class="layout column align-center">
                  <v-icon large class="deep-orange--text">person_add</v-icon>
                  <h1 class="flex my-4 primary--text">New Customer Registration</h1>
                  <h3>BestDealPharma.com is your life insurance</h3>
                  <p class="pt-5">
                    By creating an account with our store, you will be able to move through the checkout process faster,
                    store multiple shipping addresses, view and track your orders in your account and more.
                  </p>
                </div>
                <div class="text-xs-right">
                  <v-btn color="accent" class="white--text" to="/register">Register</v-btn>
                </div>
              </v-card-text>
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
      rules: {
        required: value => !!value || 'This field is required.',
        email: value => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || 'E-mail must be valid.'
        }
      },
      showLoginError: false,
      loginFormIsValid: false,
      loginLoading: false,
      loginModel: {
        email: null,
        password: null,
        forAdminPanel: false
      }

    }),

    computed: {},

    methods: {
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
              this.$store.dispatch('user/getAuthenticatedUser')
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
