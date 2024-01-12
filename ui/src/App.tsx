import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { ThemeProvider } from '@mui/material/styles'
import { createTheme } from "@mui/material/styles";
import { useMemo } from "react";
import './App.css'
import { themeSettings } from "./theme"
import PlaylistPage from './pages/homePage'

function App() {
  const theme = useMemo(() => createTheme(), []);

  return (
    <div className='app'>
      <BrowserRouter>
      <ThemeProvider theme={theme}>
          <Routes>
            <Route 
              path='/playlists' 
              element={ <PlaylistPage /> } />
          </Routes>
      </ThemeProvider>
      </BrowserRouter>
    </div>
  )
}

export default App
