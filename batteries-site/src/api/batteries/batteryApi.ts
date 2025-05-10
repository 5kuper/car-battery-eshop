import httpClient from './httpClient'
import type { Battery } from './contracts/batteryApiContracts'

export const getBatteries = async (): Promise<Battery[]> => {
  const response = await httpClient.get<Battery[]>('/api/Battery')
  return response.data
}

export const getBatteryById = async (id: string): Promise<Battery> => {
  const response = await httpClient.get<Battery>(`/api/Battery/${id}`)
  return response.data
}

export const createBattery = async (data: Partial<Battery>): Promise<Battery> => {
  const response = await httpClient.post<Battery>('/api/Battery', null, { params: data })
  return response.data
}

export const updateBattery = async (id: string, data: Partial<Battery>): Promise<void> => {
  await httpClient.put(`/api/Battery/${id}`, null, { params: { ...data, id } })
}

export const deleteBattery = async (id: string): Promise<void> => {
  await httpClient.delete(`/api/Battery/${id}`)
}
