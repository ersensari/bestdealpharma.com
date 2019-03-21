<template>
  <v-flex>

    <section>
      <v-container>
        <v-layout row
                  align-center
                  justify-center
                  class="blue--text">
          <v-flex xs12 sm12 md6 v-if="!model.token">
            <v-card class="elevation-1">
              <v-card-text>
                <div class="layout column align-center">
                  <v-icon large class="deep-orange--text">lock_open</v-icon>
                  <h1 class="flex my-4 primary--text">Rescue Password</h1>
                  <h3>If you forgot your password, please enter your registered email address.</h3>
                </div>
                <v-form lazy-validation v-model="loginFormIsValid" ref="loginForm">
                  <v-text-field append-icon="person" label="Email" type="text" v-model="loginModel.email"
                                @keyup.native.enter="rescue"
                                :rules="[rules.required,rules.email]">
                  </v-text-field>
                </v-form>
                <v-alert type="error" dismissible v-model="showLoginError" transition="scale-transition">
                  Opps! Entered email address could not be verified!
                </v-alert>
                <v-alert type="success" dismissible v-model="showLoginSuccess" transition="scale-transition">
                  Success! We has been sent a rescue link to your email. Check your email account.
                </v-alert>

              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="rescue" :loading="loginLoading">Send my rescue code</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
          <v-flex xs12 sm12 md6 v-else>
            <v-card class="elevation-1">
              <v-card-text>
                <div class="layout column align-center">
                  <v-icon large class="deep-orange--text">lock_open</v-icon>
                  <h1 class="flex my-4 primary--text">Rescue Password</h1>
                  <h3>Please define your new password.</h3>
                </div>
                <v-form lazy-validation v-model="formIsValid" ref="changePasswordForm">
                  <v-text-field append-icon="person" label="Email" type="text" v-model="model.email"
                                @keyup.native.enter="rescue"
                                :rules="[rules.required,rules.email]">
                  </v-text-field>
                  <v-text-field ref="newPassword"
                                v-model="model.newPassword"
                                :append-icon="showpwd2 ? 'visibility_off' : 'visibility'"
                                :rules="[rules.required, rules.min]"
                                :type="showpwd2 ? 'text' : 'password'"
                                label="New Password"
                                hint="At least 6 characters"
                                counter
                                @click:append="showpwd2 = !showpwd2"></v-text-field>
                  <v-text-field ref="confirmpassword"
                                v-model="model.confirmPassword"
                                :append-icon="showpwd3 ? 'visibility_off' : 'visibility'"
                                :rules="[rules.required, (v) => v == this.model.newPassword || 'The passwords you entered don\'t match']"
                                :type="showpwd3 ? 'text' : 'password'"
                                label="Confirm New Password"
                                counter
                                @click:append="showpwd3 = !showpwd3"></v-text-field>
                  <v-btn class="primary" @click="changePassword">Change Password</v-btn>
                </v-form>
              </v-card-text>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </section>
  </v-flex>
</template>
<script>
  export default {
    data: () => ({
      rules:
        {
          required: value => !!value || 'This field is required.',
          min:
            v => v && v.length >= 6 || 'Min 6 characters',
          passwordMatch:
            (v) => v == this.password || 'The passwords you entered don\'t match',
          email:
            value => {
              const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return pattern.test(value) || 'E-mail must be valid.'
            }

        },
      showLoginError: false,
      showLoginSuccess: false,
      loginFormIsValid: false,
      loginLoading: false,
      loginModel: {
        email: null,
      },
      showpwd1: false,
      showpwd2: false,
      showpwd3: false,
      formIsValid: false,
      model: {
        email:null,
        newPassword: null,
        confirmPassword: null,
        token: null
      },
    }),
    methods: {
      rescue() {
        if (this.$refs.loginForm.validate()) {
          this.showLoginError = false;
          this.axios.post('/account/SendRescueCode', JSON.stringify(this.loginModel), {
            headers: {
              'Content-Type': 'application/json'
            }
          }).then(
            (response) => {
              this.showLoginSuccess = true;

              console.log(response.data.token)
            }
          ).catch(err => {
            if (err.response.status === 400) {
              this.showLoginError = true
            } else {
              console.log(err)
            }
          })
        }
      },
      changePassword: function () {
        if (this.$refs.changePasswordForm.validate()) {

          this.$store.dispatch('user/createNewPassword', this.model).then(res => {
            this.$toastr('success', 'Your password has been changed!');

          }).catch(err => {
            if (err.response.data) {
              this.$toastr('error', err.response.data[0].description);
            } else {
              this.$toastr('error', 'An error occurred! Please try again later.');
            }
          });
        }
      }
    },
    created() {
      this.model.token = this.$route.query.token;
    }
  }
</script>
<style scoped>

</style>
