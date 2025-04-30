import type { Organization } from '@/models/organization'
import type { Thread } from '@/models/thread'
import mitt from 'mitt'

type Events = {
  OrgChanged: Organization
  ThreadChanged: Thread
}

const eventBus = mitt<Events>()

export default eventBus
