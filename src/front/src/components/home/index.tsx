import React from 'react'
import DHomeCard from './components/d-home-card'

const HomeIndex = () => {
  return (
    <div className={'p-3 grid grid-cols-2 gap-6'}>
      <DHomeCard label={'Homelab'} links={undefined}></DHomeCard>
      <DHomeCard label={'Media Center'} links={undefined}></DHomeCard>
    </div>
  )
}

export default HomeIndex
