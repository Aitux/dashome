import React from 'react'
import { Card, createStyles } from '@mantine/core'
import DHomeCardLink, { DHomeCardLinkProps } from './d-home-card-link'

export interface DHomeCardProps {
  label: string,
  links?: DHomeCardLinkProps
}

const useStyles = createStyles((theme) => ({
  label: {
    background: theme.white
  }
}))

const DHomeCard = ({ label, links }: DHomeCardProps) => {
  const { classes } = useStyles()

  return (
    <Card className={'relative border-dashed border border-gray-500 overflow-visible'}>
      <div className={`${classes.label} absolute -top-6 p-1 text-2xl`}>{label}</div>
      <div className={'grid grid-cols-2 gap-3'}>
        <DHomeCardLink
          label={'Amazon'}
          icon={'/vite.svg'}
          caption={'Test'}
          link={'https://google.com'}
        />
        <DHomeCardLink
          label={'Amazon'}
          icon={'/vite.svg'}
          caption={'Test'}
          link={'https://google.com'}
        />
        <DHomeCardLink
          label={'Amazon'}
          icon={'/vite.svg'}
          caption={'Test'}
          link={'https://google.com'}
        />
        <DHomeCardLink
          label={'Amazon'}
          icon={'/vite.svg'}
          caption={'Test'}
          link={'https://google.com'}
        />
        <DHomeCardLink
          label={'Amazon'}
          icon={'/vite.svg'}
          caption={'Test'}
          link={'https://google.com'}
        /> <DHomeCardLink
        label={'Amazon'}
        icon={'/vite.svg'}
        caption={'Test'}
        link={'https://google.com'}
      />
      </div>

    </Card>
  )
}

export default DHomeCard
