export interface Profile {
    profileId: number
    name: string
    description: string | null
    isActive: boolean
    createdAt: string
}

export interface CreateProfileRequest {
    name: string
    description: string | null
}