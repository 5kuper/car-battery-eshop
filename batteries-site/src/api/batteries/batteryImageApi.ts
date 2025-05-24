import httpClient from './httpClient'

export const batteryImageApi = {
  getImageUrl: async (id: string): Promise<{ url: string }> => {
    const response = await httpClient.get<{ url: string }>(`/api/Battery/${id}/image`)
    return response.data
  },
  updateImage: async (id: string, image: File): Promise<{ url: string }> => {
    const form = new FormData()
    form.append('image', image)

    const response = await httpClient.put<{ url: string }>(`/api/Battery/${id}/image`, form, {
      headers: { 'Content-Type': undefined },
    })
    return response.data
  },
  deleteImage: async (id: string): Promise<void> => {
    await httpClient.delete(`/api/Battery/${id}/image`)
  },
}
