import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import Playlist from '../../models/playlist';

interface PlaylistsState {
    playlists: Playlist[]
}

const initialState: PlaylistsState = {
    playlists: []
}

const playlistSlice = createSlice({
    name: 'playlist',
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchPlaylists.pending, () => {
                console.log("fetchPlaylists.pending");
            })
            .addCase(fetchPlaylists.fulfilled, (state, action) => {
                state.playlists = action.payload;
            })
    }
});

export const fetchPlaylists = createAsyncThunk(
    "playlist/fetchPlaylists",
    async () => {
        const response = await fetch('https://localhost:7215/api/playlist')
        const playlists = await response.json();
        return playlists.data;
    });

export default playlistSlice.reducer;