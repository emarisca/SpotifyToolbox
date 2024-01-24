import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import Playlist from '../../models/playlist';

interface PlaylistsState {
    playlists: Playlist[]
}

const initialState: PlaylistsState = {
    playlists: []
}

interface RequestParams {
    limit: number,
    offset: number
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
                const old = state.playlists.concat(action.payload);
                console.log("action.payload")
                console.log(action.payload)
                console.log("old")
                console.log(old)
                state.playlists = old;
            })
    }
});

export const fetchPlaylists = createAsyncThunk(
    "playlist/fetchPlaylists",
    async (params: RequestParams) => {
        const { limit, offset } = params;
        const response = await fetch(`https://localhost:7215/api/playlist?limit=${limit}&offset=${offset}`, 
        {
            credentials: 'include'
        });
        const playlists = await response.json();
        return playlists.data;
    });

export default playlistSlice.reducer;