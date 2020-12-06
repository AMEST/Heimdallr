<template>
  <div align="center">
    <v-card elevation="2">
      <v-card-title>Generate password</v-card-title>
      <v-card-text>
        <v-row>
          <v-col class="pb-0">
            <v-text-field v-model="securityRequest.service" label="Service name"></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="pb-0">
            <v-text-field v-model="securityRequest.service" label="Common name"></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="pb-0">
            <v-text-field
              v-model="securityRequest.masterPassword"
              :append-icon="masterPasswordShow ? 'mdi-eye' : 'mdi-eye-off'"
              :type="masterPasswordShow ? 'text' : 'password'"
              label="Master password"
              @click:append="masterPasswordShow = !masterPasswordShow"
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="disable-top-bottom-padding">
            <v-switch v-model="securityRequest.hasLetters" inset label="Include letters"></v-switch>
          </v-col>
          <v-col class="disable-top-bottom-padding">
            <v-switch v-model="securityRequest.hasNumeric" inset label="Include numbers"></v-switch>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="disable-top-bottom-padding">
            <v-switch v-model="securityRequest.hasSymbols" inset label="Include symbols"></v-switch>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="col-lg-12 disable-top-bottom-padding">
            <div style="text-align:left;font-size: 16px;">Password lenght:</div>
            <number-input
              v-model="securityRequest.lenght"
              :min="8"
              :max="32"
              placeholder="Password lenght"
              controls
            ></number-input>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="col-lg-12 disable-top-bottom-padding">
            <div style="text-align:left;font-size: 16px;">Version:</div>
            <number-input v-model="securityRequest.version" :min="1" placeholder="Version" controls></number-input>
          </v-col>
        </v-row>
        <div align="right">
          <v-btn class="ma-2" outlined color="indigo">Generate</v-btn>
        </div>
      </v-card-text>
    </v-card>
  </div>
</template>

<script>
import VueNumberInput from "@chenfengyuan/vue-number-input";
export default {
  name: "PasswordGenerate",
  data: () => {
    return {
      securityRequest: {
        service: "",
        commonName: "",
        masterPassword: "",
        hasLetters: true,
        hasNumeric: true,
        hasSymbols: true,
        lenght: 16,
        version: 1
      },
      masterPasswordShow: false
    };
  }
};
</script>
<style>
.disable-top-bottom-padding {
  padding-top: 0;
  padding-bottom: 0;
}
</style>