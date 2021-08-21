<template>
  <div>
    <h4>Validation Sample</h4>
    <hr class="mb-5" />
    <div v-if="message" class="alert alert-success" role="alert">
      {{ message }}
    </div>
    <div v-if="error" class="alert alert-danger" role="alert">
      {{ error }}
    </div>
    <div>
      <label>Name: </label>
      <div :class="{ error: v$.name.$errors.length }">
        <input
          v-model="model.name"
          name="name"
          type="text"
          class="form-control"
          :class="{ 'is-invalid': v$.name.$errors.length }"
        >
        <div v-for="error of v$.name.$errors" :key="error.$uid" class="input-errors">
          <div class="invalid-feedback">
            {{ error.$message }}
          </div>
        </div>
      </div>
    </div>

    <div>
      <label>Age: </label>
      <div :class="{ error: v$.age.$errors.length }">
        <input
          v-model="model.age"
          name="age"
          type="number"
          class="form-control"
          :class="{ 'is-invalid': v$.age.$errors.length }"
        >
        <div v-for="error of v$.age.$errors" :key="error.$uid" class="input-errors">
          <div class="invalid-feedback">
            {{ error.$message }}
          </div>
        </div>
      </div>
    </div>

    <div>
      <label>Email: </label>
      <div :class="{ error: v$.emailAddress.$errors.length }">
        <input
          v-model="model.emailAddress"
          name="emailAddress"
          type="email"
          class="form-control"
          :class="{ 'is-invalid': v$.emailAddress.$errors.length }"
        >
        <div v-for="error of v$.emailAddress.$errors" :key="error.$uid" class="input-errors">
          <div class="invalid-feedback">
            {{ error.$message }}
          </div>
        </div>
      </div>
    </div>

    <button class="mr-1 btn btn-primary" @click="onSubmit">
      Save
    </button>
  </div>
  {{ v$ }}
</template>

<script setup lang="ts">
import axios from 'axios'
import { useVuelidate } from '@vuelidate/core'
import { email, between, integer, required } from '@vuelidate/validators'

import type { Person } from '~/models/Person'

const message = ref('')
const error = ref('')
const model = reactive({ name: 'Isadora Jarr', age: 39, emailAddress: 'im@nymous.com' } as Person)

const rules = {
  name: { required },
  age: { required, integer, between: between(0, 150) },
  emailAddress: { required, email }
}
const $externalResults = ref({})

const v$ = useVuelidate(rules, model, { $externalResults, $autoDirty: true })

const onSubmit = async() => {
  message.value = ''
  error.value = ''
  const isFormCorrect = await v$.value.$validate()
  if (!isFormCorrect) return
  try {
    const response = await axios.post('api/person', model)
    message.value = response.data.successMessage
  }
  catch (ex) {
    error.value = ex.response.data.errorMessage

    $externalResults.value = ex.response.data.validationErrors

    // actions.setErrors(ex.response.data.validationErrors)
    const x = document.getElementsByName(Object.keys(ex.response.data.validationErrors)[0])[0]
    x.focus()
  }
}
</script>

<style lang="postcss" scoped>
.is-invalid {
  @apply border-red-300;
}
</style>
