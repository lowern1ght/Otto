import React from 'react'
import ReactDOM from 'react-dom/client'
import {AppPage} from "./AppPage.tsx";

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <AppPage title={"Otto"}/>
  </React.StrictMode>,
)
