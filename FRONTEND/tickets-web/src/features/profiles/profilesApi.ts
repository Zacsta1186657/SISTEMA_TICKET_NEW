import { apiFetch } from "../../lib/api";
import type { CreateProfileRequest, Profile } from "./types"

export function getProfiles() {
    return apiFetch<Profile[]>("/api/perfiles")
}

export function createProfile(data: CreateProfileRequest) {
    return apiFetch<{ profileId: number }>("/api/perfiles", {
        method: "POST",
        body: JSON.stringify(data)
    })
}