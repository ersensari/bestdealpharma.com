<template>
  <v-flex>
 
    <section>
      <v-container>
        <v-layout column
                  align-center
                  justify-center
                  class="blue--text">
          <v-flex xs12 sm12 md5 lg6>
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
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="rescue" :loading="loginLoading">Send my rescue code</v-btn>
              </v-card-actions>
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
      }

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
              console.log(response.data.token)
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
<style scoped>

</style>
