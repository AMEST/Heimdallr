<template>
  <v-dialog v-model="dialog" max-width="600" @input="$emit('close')">
    <v-card>
      <v-card-title>
        Password Generation History
        <v-btn icon @click="clearHistory">
          <v-icon>mdi-delete</v-icon>
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn icon @click="$emit('close')">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>
      <v-card-text>
        <v-list v-if="history.length > 0">
          <v-list-item
            v-for="(item, index) in history"
            :key="index"
            @click="applyHistory(item)"
          >
            <v-list-item-content>
              <v-list-item-title>{{ item.serviceName }}</v-list-item-title>
              <v-list-item-subtitle>
                {{ item.commonName }} - v{{ item.version }}
              </v-list-item-subtitle>
            </v-list-item-content>
            <v-list-item-action>
              <v-btn icon @click.stop="deleteHistoryItem(index)">
                <v-icon>mdi-close</v-icon>
              </v-btn>
            </v-list-item-action>
          </v-list-item>
        </v-list>
        <v-alert
          v-else
          type="info"
          border="left"
          colored-border
          elevation="2"
        >
          History is empty
        </v-alert>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    dialog: {
      type: Boolean,
      required: true
    },
    history: {
      type: Array,
      required: true
    }
  },
  methods: {
    applyHistory(item) {
      this.$emit('apply-history', item);
    },
    deleteHistoryItem(index) {
      this.$emit('delete-history-item', index);
    },
    clearHistory() {
      this.$emit('clear-history');
    }
  }
};
</script>
