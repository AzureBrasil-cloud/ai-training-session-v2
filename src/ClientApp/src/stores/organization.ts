import { ref } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'
import type { Organization } from '@/models/organization'

const useOrganizationStore = defineStore('organization', () => {
  const organization = ref<Organization | null>(null)

  const getOrganization = async () => {
    if (organization.value != null) {
      return organization
    }

    try {
      const response = await axios.get<Organization>('/api/organizations')
      organization.value = response.data
      return organization
    } catch (error) {
      console.error('Error fetching organization:', error)
      organization.value = null
      return null
    }
  }

  return { organization, getOrganization }
})

export { useOrganizationStore }
