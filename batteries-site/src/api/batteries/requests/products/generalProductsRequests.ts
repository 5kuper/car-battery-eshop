import httpClient from '../../httpClient'
import type { Response } from '../../response'

export const generalProductsRequests = {
  getImageUrl: async (id: string): Promise<Response<{ url: string }>> => {
    const result = await httpClient.get<Response<{ url: string }>>(`/api/Products/${id}/image`)
    return result.data
  },
  updateImage: async (id: string, image: File): Promise<Response<{ url: string }>> => {
    const form = new FormData()
    form.append('image', image)
    const result = await httpClient.put<Response<{ url: string }>>(
      `/api/Products/${id}/image`,
      form,
      {
        headers: { 'Content-Type': undefined },
      },
    )
    return result.data
  },
  deleteImage: async (id: string): Promise<Response<void>> => {
    const result = await httpClient.delete(`/api/Products/${id}/image`)
    return result.data
  },
}
