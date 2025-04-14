<template>
  <div align="center">
    <v-card elevation="2">
      <v-card-title>
        Generate password
        <v-spacer></v-spacer>
        <v-btn icon @click="openHistory" class="ml-2">
          <v-icon>mdi-history</v-icon>
        </v-btn>
      </v-card-title>
      <v-card-text>
        <v-form ref="form" v-model="formValidation.state" lazy-validation>
          <v-row>
            <v-col class="pb-0">
              <v-text-field
                prepend-icon="mdi-web"
                v-model="securityRequest.serviceName"
                label="Service name"
                :rules="formValidation.nameRules"
                required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="pb-0">
              <v-text-field
                prepend-icon="mdi-account"
                v-model="securityRequest.commonName"
                label="Common name"
                :rules="formValidation.nameRules"
                required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="pb-0">
              <v-text-field
                prepend-icon="mdi-lock"
                v-model="securityRequest.masterPassword"
                :append-icon="masterPasswordShow ? 'mdi-eye' : 'mdi-eye-off'"
                :type="masterPasswordShow ? 'text' : 'password'"
                label="Master password"
                @click:append="masterPasswordShow = !masterPasswordShow"
                :rules="formValidation.masterPasswordRule"
                required
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
              <number-input
                v-model="securityRequest.version"
                :min="1"
                placeholder="Version"
                controls
              ></number-input>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="col-lg-12 disable-top-bottom-padding">
              <v-checkbox
                v-model="saveToHistory"
                label="Save to history"
              ></v-checkbox>
            </v-col>
          </v-row>
        </v-form>
        <div class="mt-2" align="right" v-if="!firstGenerated">
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
    <v-alert
      v-if="error.state"
      border="right"
      colored-border
      type="error"
      elevation="4"
    >{{error.text}}</v-alert>

    <history-modal
      :dialog="historyDialog"
      :history="history"
      @apply-history="applyHistory"
      @delete-history-item="deleteHistoryItem"
      @clear-history="clearHistory"
      @close="historyDialog = false"
    />
  </div>
</template>

<script>
// eslint-disable-next-line
import VueNumberInput from "@chenfengyuan/vue-number-input";
import HistoryModal from "./HistoryModal.vue";
export default {
  name: "PasswordGenerate",
  components: {
    HistoryModal
  },
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
      saveToHistory: localStorage.getItem('saveToHistoryEnabled') === 'true' || false,
      historyDialog: false,
      history: [],
      masterPasswordShow: false,
      firstGenerated: false,
      generatedPassword: "",
      generatedPasswordShow: false,
      error: {
        state: false,
        timeout: null,
        text: ""
      },
      formValidation: {
        state: true,
        nameRules: [v => !!v || "Field is required"],
        masterPasswordRule: [
          v => !!v || "MasterPassword is required",
          v => (v && v.length >= 8) || "MasterPassword must be more than 7 characters",
          v => (v && v.length <= 32) || "MasterPassword must be less than 32 characters"
        ]
      }
    };
  },
  watch: {
    saveToHistory: function(newVal) {
      localStorage.setItem('saveToHistoryEnabled', newVal);
    }
  },
  methods: {
    Generate: function() {
      if(!this.$refs.form.validate())
        return;
      var self = this;
      var request = new XMLHttpRequest();
      request.open("POST", "/api/password");
      request.setRequestHeader("Content-Type", "application/json");
      request.onload = function() {
        var result = JSON.parse(request.response);
        if (request.status != 200) {
          if (request.status == 400) {
            var alertText = "";
            for (var error in result.errors)
              alertText += result.errors[error].join("\n") + "\n";
            self.ShowAlert(alertText);
          }
          return;
        }

        self.generatedPassword = result.generatedPassword;
        self.firstGenerated = true;
        
        if (self.saveToHistory) {
          const history = JSON.parse(localStorage.getItem('generateHistory') || '[]');
          const existingIndex = history.findIndex(item => 
            item.serviceName === self.securityRequest.serviceName &&
            item.commonName === self.securityRequest.commonName
          );

          const historyItem = {
            serviceName: self.securityRequest.serviceName,
            commonName: self.securityRequest.commonName,
            version: self.securityRequest.version,
            hasLetters: self.securityRequest.hasLetters,
            hasNumeric: self.securityRequest.hasNumeric,
            hasSpecialSymbols: self.securityRequest.hasSpecialSymbols,
            length: self.securityRequest.length
          };

          if (existingIndex !== -1) {
            history[existingIndex] = historyItem;
          } else {
            history.push(historyItem);
          }

          localStorage.setItem('generateHistory', JSON.stringify(history));
        }
      };
      request.send(JSON.stringify(this.securityRequest));
    },
    Cancel: function() {
      this.firstGenerated = false;
      this.generatedPassword = "";
    },
    CopyToClipboard: function() {
      /*eslint-disable*/
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
        /*eslint-enable*/
      }
    },
    ShowAlert: function(text) {
      var self = this;
      if (this.error.timeout != null) clearTimeout(this.error.timeout);
      this.error.state = true;
      (this.error.text = text),
        (this.error.timeout = setTimeout(function() {
          self.error.timeout = null;
          self.error.state = false;
          self.error.text = "";
        }, 15000));
    },
    openHistory: function() {
      this.history = JSON.parse(localStorage.getItem('generateHistory') || '[]');
      this.historyDialog = true;
    },
    applyHistory: function(item) {
      this.securityRequest.serviceName = item.serviceName;
      this.securityRequest.commonName = item.commonName;
      this.securityRequest.version = item.version;
      this.securityRequest.hasLetters = item.hasLetters;
      this.securityRequest.hasNumeric = item.hasNumeric;
      this.securityRequest.hasSpecialSymbols = item.hasSpecialSymbols;
      this.securityRequest.length = item.length;
      this.historyDialog = false;
    },
    deleteHistoryItem: function(index) {
      this.history.splice(index, 1);
      localStorage.setItem('generateHistory', JSON.stringify(this.history));
    },
    clearHistory: function() {
      this.history = [];
      localStorage.removeItem('generateHistory');
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
