import React from 'react'
import { VariantGet } from '~/Models/Variant'
import Variant from '~/pages/admin/variant/Variant'
import VariantItem from './VariantItem'

export type Props = {
    variantList: VariantGet[]
    col: number;
}

const VariantList = ({ variantList, col }: Props) => {
    return <div className="row px-xl-5">
        {variantList.map(variant => <VariantItem col={col} variant={variant} />)}
    </div>
}

export default VariantList
