# IDiscriminatedElementに対するテストに関して
IsLogicallyEquivalentToメソッドとIsPhysicallyEquivalentToメソッドは他のテストにその妥当性を依存させない形でテストを構成する必要がある。
この意味するところはどちらのメソッドも任意のIDiscriminatedElementの等価性を判定するためのものであり、その他のテスト内で利用されることが容易に想定できる。
この場合、この2つのテストを他の実装に対して過度に依存させる形を取ってしまうと、実装したテストに対する妥当性がある意味旬刊参照してしまう形になりテストの実行結果に対する妥当性を担保できない事態が容易に想定できる。
従って、この2つのメソッドは可能な限り独立する形でテストさえる必要がある。

# IDiscriminatedElementの同一性の確認に関して
任意のIDiscriminatedElementのインスタンスA,Bが同一か否かを確認するのは以下の2つのメソッドを利用する。

## IsLogicallyEquivalentTo
このメソッドは、完全修飾形に基づき任意のElementが同一の型並びにIdentityを持っており、なおかつそれぞれのAncestorに対しても左記の条件を満たす場合に真を返し、それ以外の場合は偽を返す。ただし、下記のIsPhysicallyEquivalentToメソッドとは異なり、物理的にどのファイルに存在しているか、また将来的にはどのバージョン管理に基づいているかなどの外部環境が一致しているか否かは評価しない。

## IsPhisicallyEquivalentTo
このメソッドは、先のIsLogicalEquibalentToの条件を満たし、その上で物理的な存在しているファイルのパスを含めて一致しているのなら真、それ以外は偽となる。

## 検討事項
将来的にVersionControlを含めて管理した場合、VersionControlの同一性を含めたEquivalentのSemanticsをどのように構築していくのか検討する必要があるかもしれない。
現在考え得る実装方法としては、
* IsPhysicallyEquivalentToに含める
* 別のメソッドを用意する

が選択しうる実装法だと考えられる。
IsPhysicallyEquivalentToメソッドにSemanticasをMergeしてしまう方法は、メソッドの意味を整理する必要がない利点がある反面、物理的なファイルとVersionControlの一致が理論積で提供される手段しか存在しないため、もしかしたら問題が起きるかもしれない。
他のメソッドを別途用意した上で実装する場合は、意味の分離がなされて柔軟性が向上する反面、それぞれのEquivlentのSemanticsを整理した上で各メソッドの関係性に矛盾が生じないように調整する必要がある。

その意味においては、ProviderとしてというよりはConsumerとして利用したときに、ExistsFileのPathとVersionControlの一致を弁別するシナリオがあるのか、あるとしてどの程度重要なのかという判断が必要になってくると思われる。

## 等価性の包含関係
IsLogicallyEquivalentToメソッドとIsPhysicallyEquivalentToメソッドはIsPhysicallyEquivalentToが真ならばIsLogicallyEquivalentToも真となる。同様にIsLogicallyEquivalentToが偽なら、IsPhysicallyEquivalentToも偽となる。
（IsPhysicallyEquivalentTo　∋　IsLogicallyEquivalentTo）