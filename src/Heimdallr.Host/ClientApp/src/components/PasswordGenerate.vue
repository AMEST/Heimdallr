<template>
  <div align="center">
    <v-card elevation="2">
      <v-card-title>Generate password</v-card-title>
      <v-card-text>
        <v-row>
          <v-col class="pb-0">
            <v-text-field v-model="securityRequest.serviceName" label="Service name"></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="pb-0">
            <v-text-field v-model="securityRequest.commonName" label="Common name"></v-text-field>
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
            <v-switch v-model="securityRequest.hasSpecialSymbols" inset label="Include symbols"></v-switch>
          </v-col>
        </v-row>
        <v-row>
          <v-col class="col-lg-12 disable-top-bottom-padding">
            <div style="text-align:left;font-size: 16px;">Password length:</div>
            <number-input
              v-model="securityRequest.length"
              :min="8"
              :max="32"
              placeholder="Password length"
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
        <div align="right" v-if="!firstGenerated">
          <v-btn class="ma-2" outlined color="indigo" @click="Generate">Generate</v-btn>
        </div>
        <v-row class="pt-4 pl-3" v-if="firstGenerated">
          <v-btn tile color="success" @click="CopyToClipboard">
            <v-icon>mdi-clipboard-multiple-outline</v-icon>
          </v-btn>
          <v-text-field
            v-model="generatedPassword"
            :append-icon="generatedPasswordShow ? 'mdi-eye' : 'mdi-eye-off'"
            :type="generatedPasswordShow ? 'text' : 'password'"
            label="Generated password"
            @click:append="generatedPasswordShow = !generatedPasswordShow"
            class="pt-0"
          ></v-text-field>
          <v-btn tile color="error" @click="Cancel">
            <v-icon>mdi-cancel</v-icon>
          </v-btn>
        </v-row>
      </v-card-text>
    </v-card>
  </div>
</template>

<script>
// eslint-disable-next-line
import VueNumberInput from "@chenfengyuan/vue-number-input";
export default {
  name: "PasswordGenerate",
  data: () => {
    return {
      securityRequest: {
        serviceName: "",
        commonName: "",
        masterPassword: "",
        hasLetters: true,
        hasNumeric: true,
        hasSpecialSymbols: true,
        length: 16,
        version: 1
      },
      masterPasswordShow: false,
      firstGenerated: false,
      generatedPassword: "",
      generatedPasswordShow: false
    };
  },
  methods: {
    Generate: function() {
      var self = this;
      var request = new XMLHttpRequest();
      request.open("POST", "/api/password");
      request.setRequestHeader("Content-Type", "application/json");
      request.onload = function() {
        var result = JSON.parse(request.response);
        console.info("[Result]", result);
        self.generatedPassword = result.generatedPassword;
        self.firstGenerated = true;
      };
      request.send(JSON.stringify(this.securityRequest));
    },
    Cancel: function() {
      this.firstGenerated = false;
      this.generatedPassword = "";
    },
    CopyToClipboard: function() {
      if (window.clipboardData && window.clipboardData.setData) {
        // IE specific code path to prevent textarea being shown while dialog is visible.
        return window.clipboardData.setData("Text", this.generatedPassword);
      } else if (
        document.queryCommandSupported &&
        document.queryCommandSupported("copy")
      ) {
        var textarea = document.createElement("textarea");
        textarea.textContent = this.generatedPassword;
        textarea.style.position = "fixed"; // Prevent scrolling to bottom of page in MS Edge.
        document.body.appendChild(textarea);
        textarea.select();
        try {
          return document.execCommand("copy"); // Security exception may be thrown by some browsers.
        } catch (ex) {
          console.warn("Copy to clipboard failed.", ex);
          return false;
        } finally {
          document.body.removeChild(textarea);
        }
      }
    }
  }
};
</script>
<style>
.disable-top-bottom-padding {
  padding-top: 0;
  padding-bottom: 0;
}
</style>