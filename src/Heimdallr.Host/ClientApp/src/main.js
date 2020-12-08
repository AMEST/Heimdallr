import Vue from 'vue'
import App from './App.vue'
import router from './router'
import './registerServiceWorker'
import vuetify from './plugins/vuetify'
import VueNumberInput from '@chenfengyuan/vue-number-input'

Vue.config.productionTip = false
Vue.use(VueNumberInput)
new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
