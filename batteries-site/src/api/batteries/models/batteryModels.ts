export interface BatterySpecs {
  voltage: number
  capacity: number
  startPower: number
  startPowerRating: StartPowerRating
}

export enum StartPowerRating {
  SAE,
  EN,
  IEC,
  DIN,
}

export interface Battery {
  id: string
  created: string
  updated: string
  name: string
  country?: string
  inStock: boolean
  price: number
  warrantyMonths: number
  imageUrl?: string
  model?: string
  specs: BatterySpecs
  tags: string[]
}

export interface BatteryForm {
  name: string
  country?: string
  inStock: boolean
  price: number
  warrantyMonths: number

  model?: string
  specs: BatterySpecs
  tags: string[]
}

export function mapBatteryToForm(battery: Battery): BatteryForm {
  return {
    name: battery.name,
    country: battery.country,
    inStock: battery.inStock,
    price: battery.price,
    warrantyMonths: battery.warrantyMonths,
    model: battery.model,
    specs: battery.specs,
    tags: battery.tags,
  }
}
