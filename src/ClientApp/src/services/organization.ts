import type { Organization } from '@/models/organization'
import eventBus from '@/services/eventBus'
import axios, { AxiosError } from 'axios'

const service = {
  save: async (org: Organization) => {
    try {
      const url = org.id ? `/api/organizations/${org.id}` : '/api/organizations'
      const method = org.id ? 'put' : 'post'
      const response = await axios[method]<Organization>(url, org)
      eventBus.emit('OrgChanged', org)
      return response.data
    } catch (error: unknown) {
      const axiosError = error as AxiosError
      throw axiosError?.response?.data
    }
  },
}

export default service
