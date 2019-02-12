<template>
  <v-flex>
    <v-card>
      <v-card-title>
        <div class="headline">
          <v-icon large>perm_identity</v-icon>
          Information
        </div>
      </v-card-title>
      <v-divider></v-divider>
      <v-list v-if="authenticatedUser">
        <v-list-tile>
          <v-list-tile-content>E-mail:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.user.email }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>Mobile Phone:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.mobilePhone }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>Home Phone:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.homePhone }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>Date of Birth:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.birthDate | formatdate }}
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>Address:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.address }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>City:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.city }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>State:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.state }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>Zip Code:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.zipCode }}</v-list-tile-content>
        </v-list-tile>
        <v-list-tile>
          <v-list-tile-content>Country:</v-list-tile-content>
          <v-list-tile-content class="align-end">{{ authenticatedUser.person.country }}</v-list-tile-content>
        </v-list-tile>
        <v-divider></v-divider>
        <v-card class="elevation-0">
          <v-card-title>
            <div class="headline">
              <v-icon large>lock</v-icon>
              Change Password
            </div>
          </v-card-title>

          <v-flex class="xs6 pa-3">
            <v-text-field ref="currentPassword"
                          v-model="model.currentPassword"
                          :append-icon="showpwd1 ? 'visibility_off' : 'visibility'"
                          :rules="[rules.required, rules.min]"
                          :type="showpwd1 ? 'text' : 'password'"
                          label="Current Password"
                          hint="At least 6 characters"
                          counter
                          @click:append="showpwd1 = !showpwd1"></v-text-field>
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
            <v-btn class="primary">Change Password</v-btn>
            <v-list color="error transparent" v-model="showChangePasswordError" transition="scale-transition">
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
          </v-flex>
        </v-card>
      </v-list>
    </v-card>
  </v-flex>

</template>

<script>
  import {mapGetters} from "vuex";

  export default {
    computed: {
      ...mapGetters('user', ['getAuthenticatedUserName', 'isAuthenticated', 'authenticatedUser'])
    },
    data: () => ({
      showChangePasswordError: false,
      errorMessages: [],
      model: {
        currentPassword: null,
        newPasword: null,
        confirmPassword: null
      },
      showpwd1: false,
      showpwd2: false,
      showpwd3: false,
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

        }
    })

  }
</script>

<style scoped>

</style>
